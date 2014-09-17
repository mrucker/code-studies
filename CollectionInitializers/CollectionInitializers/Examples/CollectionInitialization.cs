using System;
using System.Collections;
using System.Collections.Generic;

namespace ObjectAndCollectionInitializers.Examples
{
    public class CollectionInitialization
    {
        public void ListInitialization()
        {
            //List initialization can be used on arrays of any type as long as there is a unifying type in the initializer
            var a = new[] {1, 2};
            var b = new[] {new ArrayList(), new ArrayList()};
            var c = new[] {(IEnumerable) new ArrayList(), (IEnumerable) new List<int>()};
            
            //it doesn't appear that you can initializes double arrays
            //var d = new[][] {new[]{1,2,3,4}, new[]{4,5,6,7}};

            //List initialization can be used on any type that implements IEnumerable and has an Add() method
            //The Add() method can't be implemented as an extension method if you want to use list initialization
            var li = new List<int> {1, 2, 3, 4};
            var m1 = new MyList {1, 2, 3, 4};
            var m2 = new MyList("John") {1, 2, 3, 4};
        }

        public void DictionaryInitialization()
        {
            //dictionaries can be initialized like this with any type
            var a = new Dictionary<int, int>
            {
                {1, 1},
                {1, 2}
            };

            //any class that implements IEnumerable and has a 2 parameter Add() method can be initialized like this
            var b = new MyDictionary()
            {
                {1, 1},
                {2, 2}
            };

            //any class that implements IEnumerable and has an Add() method with any number of parameters can be initialized
            var c = new MyThreeParameterAdd()
            {
                {"a", 2, 'c'}
            };
        }

        #region My Collections
        public class MyList : IEnumerable
        {
            public MyList(){}

            public MyList(string name){}

            public bool Add(object item)
            {
                throw new NotImplementedException();
            }

            public IEnumerator GetEnumerator()
            {
                throw new NotImplementedException();
            }
        }

        public class MyDictionary : IEnumerable
        {
            public bool Add(int key, int value)
            {
                throw new NotImplementedException();
            }

            public IEnumerator GetEnumerator()
            {
                throw new NotImplementedException();
            }
        }

        public class MyThreeParameterAdd : IEnumerable
        {
            public bool Add(object one, object two, object three)
            {
                throw new NotImplementedException();
            }

            public IEnumerator GetEnumerator()
            {
                throw new NotImplementedException();
            }
        }
        #endregion
    }
}
