#region Using

using System;

#endregion

namespace PuppyFramework.Models
{
    public class RecentFileInfo : EventArgs
    {
        #region Fields

        private readonly Uri _uri;

        #endregion Fields

        #region Properties

        public string LongName
        {
            get { return _uri.LocalPath; }
        }

        public string ShortName
        {
            get
            {
                var uriSegments = _uri.Segments[_uri.Segments.Length - 1];
                return Uri.UnescapeDataString(uriSegments).Replace("_", "__");
            }
        }

        public Uri Uri
        {
            get { return _uri; }
        }

        #endregion Properties

        #region Constructors

        public RecentFileInfo(string path)
        {
            _uri = new Uri(path);
        }

        #endregion Constructors

        #region Methods

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            return Uri.Equals(((RecentFileInfo) obj).Uri);
        }

        public override int GetHashCode()
        {
            return Uri.GetHashCode();
        }

        #endregion Methods
    }
}