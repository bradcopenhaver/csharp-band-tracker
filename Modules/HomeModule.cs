using Nancy;
using System;
using System.Collections.Generic;
using BandTracker.Objects;

namespace BandTracker
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        List<Band> allBands = Band.GetAll();
        List<Venue> allVenues = Venue.GetAll();
        model.Add("bands", allBands);
        model.Add("venues", allVenues);
        return View["index.cshtml", model];
      };
      Get["/band/new"] = _ => {
        return View["new-band-form.cshtml"];
      };
      Get["/venue/new"] = _ => {
        return View["new-venue-form.cshtml"];
      };
      Post["/band/new"] = _ => {
        Band newBand = new Band(Request.Form["bandName"]);
        newBand.Save();
        return View["created.cshtml", newBand];
      };
      Post["/venue/new"] = _ => {
        Venue newVenue = new Venue(Request.Form["venueName"]);
        newVenue.Save();
        return View["created.cshtml", newVenue];
      };
    }
  }
}
