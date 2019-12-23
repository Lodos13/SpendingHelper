using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    static public class CSConverterToObservableCollection
    {
        static public ObservableCollection<T> ConvertList<T>(List<T> list)
        {
            ObservableCollection<T> resultCollection = new ObservableCollection<T>();
            foreach (T element in list)
                resultCollection.Add(element);

            return resultCollection;
        }
    }
}
