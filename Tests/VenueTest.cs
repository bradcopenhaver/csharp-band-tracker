using Xunit;
using System;
using System.Collections.Generic;
using BandTracker.Objects;

namespace BandTracker
{
  public class VenueTest : IDisposable
  {
    public void Dispose()
    {
      Venue.DeleteAll();
    }
    public VenueTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=band_tracker_test;Integrated Security=SSPI;";
    }
    [Fact]
    public void GetAll_DatabaseEmpty_true()
    {
      //Arrange
      //Act
      int result = Venue.GetAll().Count;
      //Assert
      Assert.Equal(0, result);
    }
  }
}
