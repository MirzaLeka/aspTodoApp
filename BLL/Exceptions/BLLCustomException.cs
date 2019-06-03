using Product.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Exceptions
{
	public class BLLCustomException : BaseException
	{
		public BLLCustomException(string message) : base(message)
		{

		}

		public BLLCustomException(string message, Exception ex) : base(message, ex)
		{

		}
	}
}
