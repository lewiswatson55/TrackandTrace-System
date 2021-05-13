using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusinessLayer;

namespace UnitTest_Location
{
    [TestClass]
    public class UnitTest1
    {
        /* 
         * Author:              Lewis Watson - 40432878
         * Description:         This is a unit test class, I will be testing the Location Class.
         * Date modified:       7/12/2020
        */

        [TestMethod]
        public void TestLocation_Class()
        {

            //Arrange
            string expected_name = "Student Flat";
            string expected_address = "27/4 Msd";
            int expected_id = 0;

            //Act
            Location loc = new Location(0);
            loc.Address = "27/4 Msd";
            loc.Name = "Student Flat";

            //Assert
            string actual_name = loc.Name;
            string actual_address = loc.Address;
            int actual_id = loc.Location_id;

            Assert.AreEqual(expected_id, actual_id, 0.001, "Constructor failed to add location_id.");
            Assert.AreEqual(expected_name, actual_name, "Get/Set for Name failed.");
            Assert.AreEqual(expected_address, actual_address, "Get/Set for Address failed.");

        }
    }
}
