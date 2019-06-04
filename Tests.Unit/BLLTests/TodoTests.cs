using BLL.Exceptions;
using BLL.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Tests.Unit.BLLTests
{

	public class TodoTests
	{
		TodoBLL todoBLL;

		public TodoTests()
		{
			todoBLL = new TodoBLL();
		}

		[Theory]
		[InlineData(1)]
		public void ValidateTodoId_ShouldWorkIfIdIsPositiveNumber(int ID)
		{
			bool result = todoBLL.ValidateTodoId(ID);

			Assert.True(result);
		}

		[Theory]
		[InlineData(0)]
		[InlineData(-1)]
		public void ValidateTodoId_ShouldFailIfIdIsZeroOrLower(int ID)
		{
			Assert.Throws<ArgumentException>(() => todoBLL.ValidateTodoId(ID));
		}


		[Theory]
		[InlineData("My first todo")]
		public void ValidateTodoText_ShouldIncludeTodoText(string text)
		{
			bool result = todoBLL.ValidateTodoText(text);
			Assert.True(result);
		}

		[Theory]
		[InlineData("")]
		[InlineData(null)]
		public void ValidateTodoText_ShouldFailIfTextIsNotProvided(string text)
		{
			Assert.Throws<BLLCustomException>(() => todoBLL.ValidateTodoText(text));
		}

		[Theory]
		[InlineData("Updating todo", null)]
		[InlineData(null, true)]
		[InlineData("Updating todo", false)]
		public void ValidateTodoProperties_ShouldWorkIfAtLeastOneOfThePropertiesIsProvided(string text, bool ?completed)
		{
			bool result = todoBLL.ValidateTodoProperties(text, completed);
			Assert.True(result);
		}

		[Theory]
		[InlineData(null, null)]
		[InlineData("", null)]
		public void ValidateTodoProperties_ShouldFailIfNeitherOfThePropertiesAreProvided(string text, bool? completed)
		{
			Assert.Throws<BLLCustomException>(() => todoBLL.ValidateTodoProperties(text, completed));
		} 


	}
}
