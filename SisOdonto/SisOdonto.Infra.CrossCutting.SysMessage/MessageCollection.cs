using SisOdonto.Infra.CrossCutting.SysMessage.Enumerate;
using System.Collections;

namespace SisOdonto.Infra.CrossCutting.SysMessage
{
    public class MessageCollection : IList<MessageRecord>
    {
        private readonly IList<MessageRecord> _list = new List<MessageRecord>();

        public IEnumerator<MessageRecord> GetEnumerator() => _list.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        #region Implementation of ICollection<T>

        public void Add(MessageRecord item) => _list.Add(item);

        public void Clear() => _list.Clear();

        public bool Contains(MessageRecord item) => _list.Contains(item);

        public void CopyTo(MessageRecord[] array, int arrayIndex) => _list.CopyTo(array, arrayIndex);

        public bool Remove(MessageRecord item) => _list.Remove(item);

        public int Count => _list.Count;

        public bool IsReadOnly => _list.IsReadOnly;

        #endregion Implementation of ICollection<T>

        #region Implementation of IList<T>

        public int IndexOf(MessageRecord item) => _list.IndexOf(item);

        public void Insert(int index, MessageRecord item) => _list.Insert(index, item);

        public void RemoveAt(int index) => _list.RemoveAt(index);

        public MessageRecord this[int index]
        {
            get { return _list[index]; }
            set { _list[index] = value; }
        }

        #endregion Implementation of IList<T>

        #region Implementation Custom Methods

        public void AddValidationError(string message)
        {
            _list.Add(new MessageRecord()
            {
                Description = message,
                Type = MessageType.ValidationError
            });
        }

        public void AddSystemError(string message)
        {
            _list.Add(new MessageRecord()
            {
                Description = message,
                Type = MessageType.SystemError
            });
        }

        public void AddSuccess(string message)
        {
            _list.Add(new MessageRecord()
            {
                Description = message,
                Type = MessageType.Success
            });
        }

        public void AddWarning(string message)
        {
            _list.Add(new MessageRecord()
            {
                Description = message,
                Type = MessageType.Warning
            });
        }

        public void AddMessage(string message, MessageType messageType)
        {
            _list.Add(new MessageRecord()
            {
                Description = message,
                Type = messageType
            });
        }

        public void Copy(MessageCollection errorList)
        {
            if (errorList != null)
            {
                foreach (var item in errorList)
                {
                    switch (item.Type)
                    {
                        case MessageType.SystemError:
                            {
                                AddSystemError(item.Description);
                                break;
                            };
                        case MessageType.ValidationError:
                            {
                                AddValidationError(item.Description);
                                break;
                            };
                        case MessageType.Success:
                            {
                                AddSuccess(item.Description);
                                break;
                            }
                        default:
                            {
                                AddWarning(item.Description);
                                break;
                            }
                    }
                }
            }
        }

        public bool HasSystemError() => _list.Any(a => a.Type == MessageType.SystemError);
        public bool HasValidationError() => _list.Any(a => a.Type == MessageType.ValidationError);
        public bool HasError() => _list.Any(a => a.Type == MessageType.SystemError || a.Type == MessageType.ValidationError);

        #endregion Implementation Custom Methods
    }
}