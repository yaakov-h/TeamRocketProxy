﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using Fiddler;
using TeamRocketProxy.Integration.Http;
using TeamRocketProxy.Properties;

namespace TeamRocketProxy.Interception.Http
{
    sealed class FiddlerHttpProxy : IHttpProxy
    {
        public FiddlerHttpProxy()
        {
            FiddlerApplication.SetAppDisplayName("Team Rocket Proxy");

            FiddlerApplication.BeforeResponse += OnFiddlerResponseReceived;
            FiddlerApplication.AfterSessionComplete += OnFiddlerSessionComplete;

            FiddlerApplication.oTranscoders.ImportTranscoders("BasicFormatsForCore.dll");
            FiddlerApplication.Prefs.SetInt32Pref("fiddler.importexport.HTTPArchiveJSON.MaxTextBodyLength", OneHundredMBInBytes);

            FiddlerApplication.Log.OnLogString += (s, e) => Debug.WriteLine(e.LogString);
            FiddlerApplication.OnNotification += (s, e) => Debug.WriteLine(e.NotifyString);

            sessions = new List<Session>();
        }
        
        const int OneHundredMBInBytes = 1024 * 1024 * 100;
        readonly object sessionsLock = new object();
        readonly List<Session> sessions;
        HttpProxyConfiguration configuration;

        #region IHttpProxy

        public event EventHandler<HttpSessionCompleteEventArgs> OnSessionComplete;

        public void Configure(HttpProxyConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void Start()
        {
            var flags = FiddlerCoreStartupFlags.None;

            if (configuration.DecryptSecureConnections)
            {
                flags |= FiddlerCoreStartupFlags.DecryptSSL;
            }

            if (configuration.AllowRemoteConnections)
            {
                flags |= FiddlerCoreStartupFlags.AllowRemoteClients;
            }

            if (!InstallCertificate(Settings.Default))
            {
                MessageBox.Show("Unable to create root SSL/TLS certificate for interception.", "Fiddler Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            FiddlerApplication.Startup(configuration.PortNumber, flags);
        }

        public void Stop()
            => FiddlerApplication.Shutdown();

        public void Load(string path)
        {
            var options = new Dictionary<string, object>
            {
                ["Filename"] = path
            };
            var sessions = FiddlerApplication.DoImport("HTTPArchive v1.2", false, options, null);

            if (sessions != null)
            {
                foreach (var session in sessions)
                {
                    OnFiddlerSessionComplete(session);
                }
            }
        }

        public void Save(string path)
        {
            Session[] sessionsToSave;

            lock (sessionsLock)
            {
                sessionsToSave = sessions.ToArray();
            }

            var options = new Dictionary<string, object>
            {
                ["Filename"] = path
            };
            FiddlerApplication.DoExport("HTTPArchive v1.2", sessionsToSave, options, null);
        }

        #endregion

        #region Fiddler Events

        void OnFiddlerResponseReceived(Session oSession)
        {
            if (oSession.HTTPMethodIs("CONNECT"))
            {
                // iOS 10.0 beta bug. rdar://problem/27423535
                oSession.ResponseHeaders.Remove("Connection");
            }
        }

        void OnFiddlerSessionComplete(Session oSession)
        {
            if (configuration == null || !configuration.HostsToIntercept.Any(host => oSession.HostnameIs(host)))
            {
                return;
            }

            lock (sessionsLock)
            {
                sessions.Add(oSession);
            }

            var session = new FiddlerHttpSession(oSession);
            OnSessionComplete?.Invoke(this, new HttpSessionCompleteEventArgs(session));
        }

        #endregion

        #region SSL/TLS CA Persistence

        public static bool InstallCertificate(Settings settings)
        {
            const string FiddlerPreferenceKeyForPrivateKey = "fiddler.certmaker.bc.key";
            const string FiddlerPreferenceKeyForCertificate = "fiddler.certmaker.bc.cert";

            if (CertMaker.rootCertExists())
                return true;

            if (!string.IsNullOrEmpty(settings.FiddlerInterceptionCertificate) && !string.IsNullOrEmpty(settings.FiddlerInterceptionPrivateKey))
            {
                FiddlerApplication.Prefs.SetStringPref(FiddlerPreferenceKeyForPrivateKey, settings.FiddlerInterceptionPrivateKey);
                FiddlerApplication.Prefs.SetStringPref(FiddlerPreferenceKeyForCertificate, settings.FiddlerInterceptionCertificate);
            }

            if (!CertMaker.createRootCert())
                return false;

            // Recreate expired certificate
            var certificate = CertMaker.GetRootCertificate();
            if (certificate.NotAfter <= DateTime.Now)
            {
                FiddlerApplication.Prefs.RemovePref(FiddlerPreferenceKeyForPrivateKey);
                FiddlerApplication.Prefs.RemovePref(FiddlerPreferenceKeyForCertificate);

                if (!CertMaker.createRootCert())
                    return false;
            }

            settings.FiddlerInterceptionCertificate = FiddlerApplication.Prefs.GetStringPref(FiddlerPreferenceKeyForCertificate, null);
            settings.FiddlerInterceptionPrivateKey = FiddlerApplication.Prefs.GetStringPref(FiddlerPreferenceKeyForPrivateKey, null);
            settings.Save();

            return true;
        }

        #endregion
    }
}
