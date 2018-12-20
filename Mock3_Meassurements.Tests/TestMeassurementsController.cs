using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mock3_Meassurements.Controllers;

namespace Mock3_Meassurements.Tests
{
	[TestClass]
	public class TestMeassurementsController
	{
		[TestMethod]
		public void TestGetAll()
		{
			// Arrange
			MeassurementsController mc = new MeassurementsController();

			// Act
			List<Meassurement> lmc =  new List<Meassurement>();
			lmc = mc.Get();

			// Assert
			CollectionAssert.AllItemsAreNotNull(lmc);
		}

		[TestMethod]
		public void TestGetSingle()
		{
			// Arrange
			MeassurementsController mc = new MeassurementsController();

			// Act
			Meassurement m = mc.Get(1); // Kan være 1 ikke findes længere, ergo fail test (man tester normalt ikke på db)

			// Assert
			Assert.IsNotNull(m);
		}

		[TestMethod]
		public void TestPost()
		{
			// Arrange
			MeassurementsController mc = new MeassurementsController();

			// Act
			// Insert new M then get it and check if it exists
			Meassurement m = new Meassurement
			{
				Humidity = "20",
				Pressure = "21",
				Temperature = "22",
				TimeStamp = DateTime.Now
			};
			int newMId = mc.Post(m);

			Meassurement postedM = mc.Get(newMId);

			// Assert
			Assert.IsNotNull(postedM);
		}

		[TestMethod]
		public void TestDelete()
		{
			// Arrange
			MeassurementsController mc = new MeassurementsController();

			// Act
			// Insert new M and then delete it, then check if it exists, then check if it doesn't exist
			Meassurement m = new Meassurement
			{
				Humidity = "20",
				Pressure = "21",
				Temperature = "22",
				TimeStamp = DateTime.Now
			};
			int newMId = mc.Post(m);

			Meassurement postedM = mc.Get(newMId);

			// Assert
			if (postedM != null)
			{
				// Delete returns id of the deleted.
				int delid = mc.Delete(postedM.Id);

				// Assert that the deleted id is the same as the id of the posted M
				Assert.AreEqual(postedM.Id, delid);
			}
			else
			{
				// Failed, did not post to db
				Assert.Fail();
			}
		}
	}
}
