/*
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sohato.ElectronicPayment.TransactionsEtl.JobFramework.Redis
{
    public class RedisCache
    {
        private static readonly Lazy<ConnectionMultiplexer> _redis 
            = new Lazy<ConnectionMultiplexer>(()=> ConnectionMultiplexer.Connect("srv-eoperation")); // TODO: the address should be on web config. 

        private static readonly TimeSpan _defaultExpirationTime = new TimeSpan(24,0,0);

        public string this[string key]
        {
            get
            {
                IDatabase db = _redis.Value.GetDatabase();
                return db.StringGet(key);
            }

            set
            {
                IDatabase db = _redis.Value.GetDatabase();
                db.StringSet(key, value,_defaultExpirationTime);
            }
        }

        public bool ContainsKey(string key)
        {
            IDatabase db = _redis.Value.GetDatabase();
            return db.KeyExists(key);
        }
    }
}
*/