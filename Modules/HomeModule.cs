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
      Get["/band/{id}"] = parameters => {
        Band currentBand = Band.Find(parameters.id);
        Dictionary<string, object> model = new Dictionary<string, object>();
        model.Add("band", currentBand);
        model.Add("bandVenues", currentBand.GetVenues());
        model.Add("venues", Venue.GetAll());
        return View["band.cshtml", model];
      };
      Post["/band/{id}/add-venue"] = parameters => {
        Band currentBand = Band.Find(parameters.id);
        Venue currentVenue = Venue.Find(Request.Form["venueId"]);
        currentBand.AddVenue(Request.Form["venueId"]);
        Dictionary<string, object> model = new Dictionary<string, object>();
        model.Add("band", currentBand);
        model.Add("venue", currentVenue);
        return View["saved.cshtml", model];
      };
      Get["/venue/{id}"] = parameters => {
        Venue currentVenue = Venue.Find(parameters.id);
        Dictionary<string, object> model = new Dictionary<string, object>();
        model.Add("venue", currentVenue);
        model.Add("venueBands", currentVenue.GetBands());
        model.Add("bands", Band.GetAll());
        return View["venue.cshtml", model];
      };
      Post["/venue/{id}/add-band"] = parameters => {
        Venue currentVenue = Venue.Find(parameters.id);
        Band currentBand = Band.Find(Request.Form["bandId"]);
        currentVenue.AddBand(Request.Form["bandId"]);
        Dictionary<string, object> model = new Dictionary<string, object>();
        model.Add("band", currentBand);
        model.Add("venue", currentVenue);
        return View["saved.cshtml", model];
      };
      Get["/band/{id}/edit"] = parameters => {
        Band currentBand = Band.Find(parameters.id);
        return View["edit-band-form.cshtml", currentBand];
      };
      Get["/venue/{id}/edit"] = parameters => {
        Venue currentVenue = Venue.Find(parameters.id);
        return View["edit-venue-form.cshtml", currentVenue];
      };
      Patch["/band/{id}/edit"] = parameters => {
        Band currentBand = Band.Find(parameters.id);
        currentBand.Edit(Request.Form["newName"]);
        return View["edit-success.cshtml", currentBand];
      };
      Patch["/venue/{id}/edit"] = parameters => {
        Venue currentVenue = Venue.Find(parameters.id);
        currentVenue.Edit(Request.Form["newName"]);
        return View["edit-success.cshtml", currentVenue];
      };
    }
  }
}
