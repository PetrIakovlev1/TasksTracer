using System.Collections.Generic;
using System.DirectoryServices;
using WebRequests.Models;

namespace WebRequests.DAL
{
    public static class adReader
    {
        public static List<AdUserInfo> GetAdUserList(string SearchString)
        {
            List<AdUserInfo> adUserInfoList = new List<AdUserInfo>();

            if (!string.IsNullOrWhiteSpace(SearchString))
                if (SearchString.Length >= 2)
                {
                    DirectoryEntry directoryEntry = new DirectoryEntry("LDAP://domain", "Uname@domain", "pwd", AuthenticationTypes.Secure);
                    DirectorySearcher searcher = new DirectorySearcher(directoryEntry)
                    {
                        PageSize = int.MaxValue,
                        Filter = $"(&(objectCategory=person)(objectClass=user)(|(sn={SearchString}*)(userPrincipalName={SearchString}*)))"
                    };

                    searcher.PropertiesToLoad.Add("displayName");
                    //searcher.PropertiesToLoad.Add("proxyAddresses");
                    searcher.PropertiesToLoad.Add("mail");

                    SearchResultCollection result = searcher.FindAll();

                    foreach (SearchResult r in result)
                    {
                        AdUserInfo adUserInfo = new AdUserInfo();

                        if (r.Properties.Contains("displayname"))
                            adUserInfo.UserName = r.Properties["displayname"][0].ToString();

                        if (r.Properties.Contains("mail"))
                            adUserInfo.Email = r.Properties["mail"][0].ToString();

                        if (!string.IsNullOrWhiteSpace(adUserInfo.Email) && !string.IsNullOrWhiteSpace(adUserInfo.UserName))
                            adUserInfoList.Add(adUserInfo);
                    }
                }

            return adUserInfoList;
        }




    }
}