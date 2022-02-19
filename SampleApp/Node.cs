using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleApp
{
    public class Node : INotifyPropertyChanged
    {
        string _name;
        public string Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Name"));
                }
            }
        }

        public string Test { get; set; }

        // AddCommand -> CommandParameter 
        public void Add(object parameter)
        {

        }

        public bool CanAdd(object parameter)
        {
            return false;
        }

        public string DisplayInfo { get; set; }

        public NodeCollection ChildNodes { get; set; }

        public NodeCollection Source { get; set; }

        public Node Parent
        {
            get
            {
                if (Source != null)
                {
                    return Source.Owner;
                }
                return null;
            }
        }

        public Node()
        {
            ChildNodes = new NodeCollection(this);
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

    public class NodeCollection : ObservableCollection<Node>
    {
        public Node Owner { get; private set; }

        public NodeCollection(Node owner)
        {
            Owner = owner;
        }

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (Node item in e.NewItems)
                {
                    item.Source = this;
                }
            }

            //if (e.OldItems != null)
            //{
            //    foreach (Node item in e.OldItems)
            //    {
            //        item.Source = null;
            //    }
            //}

            base.OnCollectionChanged(e);
        }
    }
}
