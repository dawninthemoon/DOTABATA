using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Game.StaticData
{
    public class StaticDataWrapper<T>
    {
        protected List<T> _dataList;
        public List<T> DataList => _dataList;

        public void LoadData(JObject jObject)
        {
            var jToken = jObject[typeof(T).Name];
            _dataList = JsonConvert.DeserializeObject<List<T>>(jToken.ToString());
            OnInitialized();
        }

        protected virtual void OnInitialized()
        {
        }
    }
}
