using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace Shop.Infrastructure
{
    public class SessionMenager : ISessionMenager
    {
        private HttpSessionState session;
        public SessionMenager() // controler
        {
            session = HttpContext.Current.Session;
        }

        public T Get<T>(string key) // Download session
        {
            return (T)session[key]; 
        }

        public void Set<T>(string name, T value) //Session setting
        {
            session[name] = value;
        }


        public void Abandon()// Deleting a session
        {
            session.Abandon();
        }

        public T TryGet<T>(string key) // Downloading session in try catch
        {
            try
            {
                return (T)session[key];
            }
            catch (NullReferenceException) 
            {
                return default(T);
            }
        }
    }
}