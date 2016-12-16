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
    [Fact]
    public void Equals_EqualOverride_True()
    {
      //Arrange and Act
      Venue firstVenue = new Venue("Wonder Ballroom");
      Venue secondVenue = new Venue("Wonder Ballroom");
      //Assert
      Assert.Equal(firstVenue,secondVenue);
    }
    [Fact]
    public void Save_SavesVenueToDatabase_true()
    {
      //Arrange
      Venue newVenue = new Venue("Doug Fir");
      //Act
      newVenue.Save();
      List<Venue> allVenues = Venue.GetAll();
      //Assert
      Assert.Equal(newVenue, allVenues[0]);
    }
  }
}
