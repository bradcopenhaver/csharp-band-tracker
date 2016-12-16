using Xunit;
using System;
using System.Collections.Generic;
using BandTracker.Objects;

namespace BandTracker
{
  public class BandTest : IDisposable
  {
    public void Dispose()
    {
      Band.DeleteAll();
    }
    public BandTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=band_tracker_test;Integrated Security=SSPI;";
    }
    [Fact]
    public void GetAll_DatabaseEmpty_true()
    {
      //Arrange
      //Act
      int result = Band.GetAll().Count;
      //Assert
      Assert.Equal(0, result);
    }
    [Fact]
    public void Equals_EqualOverride_True()
    {
      //Arrange and Act
      Band firstBand = new Band("Three Days Grace");
      Band secondBand = new Band("Three Days Grace");
      //Assert
      Assert.Equal(firstBand,secondBand);
    }
    [Fact]
    public void Save_SavesBandToDatabase_true()
    {
      //Arrange
      Band newBand = new Band("Sublime");
      //Act
      newBand.Save();
      List<Band> allBands = Band.GetAll();
      //Assert
      Assert.Equal(newBand, allBands[0]);
    }
  }
}
