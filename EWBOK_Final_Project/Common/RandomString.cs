using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EWBOK_Final_Project.Common
{
    public class RandomString
    {
		public string MakeRandomString(int size)
		{
			Random rand = new Random();
			// Characters we will use to generate this random string.
			char[] allowableChars = "ABCDEFGHIJKLOMNOPQRSTUVWXYZ0123456789".ToCharArray();

			// Start generating the random string.
			string activationCode = string.Empty;
			for (int i = 0; i <= size - 1; i++)
			{
				activationCode += allowableChars[rand.Next(allowableChars.Length - 1)];
			}

			// Return the random string in upper case.
			return activationCode.ToUpper();
		}
	}
}