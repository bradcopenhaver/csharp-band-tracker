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
  }
}
