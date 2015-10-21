using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Net;
using System.IO;

namespace HospitalService
{
	// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
	// NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
	public class Service1 : IService1
	{

		// Get the latitude and longitude of a given zipcode.
		public double getLatitude(string zipcode)
		{
			string zURL = string.Format("http://maps.googleapis.com/maps/api/geocode/xml?address={0}", zipcode);
			WebRequest latlongRequest = WebRequest.Create(zURL);
			Stream latlongStream = latlongRequest.GetResponse().GetResponseStream();
			XMLParser myParser = new XMLParser(latlongStream);
			return myParser.getLatitude();
		}

		public double getLongitude(string zipcode)
		{
			string zURL = string.Format("http://maps.googleapis.com/maps/api/geocode/xml?address={0}", zipcode);
			WebRequest latlongRequest = WebRequest.Create(zURL);
			Stream latlongStream = latlongRequest.GetResponse().GetResponseStream();
			XMLParser myParser = new XMLParser(latlongStream);
			return myParser.getLongitude();
		}

		public List<string> findHealthcare(string zipcode)
		{
			Tuple<double, double> latlon = new Tuple<double,double>(getLatitude(zipcode), getLongitude(zipcode));

			string hURL = string.Format("https://maps.googleapis.com/maps/api/place/nearbysearch/xml?location={0},{1}&radius=8000&types=hospital&&key=AIzaSyDi63HmrxBC4Dfu9TF_z3avqxEryE0ZaDE", latlon.Item1, latlon.Item2);
			WebRequest searchRequest = WebRequest.Create(hURL);
			Stream resultsStream = searchRequest.GetResponse().GetResponseStream();
			XMLParser myParser = new XMLParser(resultsStream);
			return myParser.parseHealthcareLocations();
		}

		public string GetData(int value)
		{
			return string.Format("You entered: {0}", value);
		}

		public CompositeType GetDataUsingDataContract(CompositeType composite)
		{
			if (composite == null)
			{
				throw new ArgumentNullException("composite");
			}
			if (composite.BoolValue)
			{
				composite.StringValue += "Suffix";
			}
			return composite;
		}
	}
}
