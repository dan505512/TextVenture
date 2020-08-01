using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TextVenture.Admin.WebView.SessionManagement
{
    internal class SessionManager
    {
        private readonly List<string> _sessions;

        public SessionManager()
        {
            _sessions = new List<string>();
        }

        /// <summary>
        /// Returns a new session ID
        /// </summary>
        /// <returns>A generated GUID of a session ID to add to the cookie</returns>
        public string GetNewSessionsId()
        {
            var sessionId = Guid.NewGuid().ToString();
            _sessions.Add(sessionId);
            return sessionId;
        }

        /// <summary>
        /// Validates the existence of a session
        /// </summary>
        /// <param name="sessionId">The session ID received from the client</param>
        /// <returns>True if exists. False otherwise</returns>
        public bool ValidateSession(string sessionId)
        {
            return _sessions.Exists(s => s.Equals(sessionId));
        }
    }

    internal static class SessionManagerFactory
    {
        private static SessionManager manager;

        public static SessionManager GetSessionManager()
        {
            if (manager == null)
            {
                manager = new SessionManager();
            }

            return manager;
        }
    }
}
