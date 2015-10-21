using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.IO;

namespace HospitalService
{
	public class XMLParser
	{
		XmlDocument xmlDoc;

		public XMLParser(Stream xmlStream)
		{
			xmlDoc = new XmlDocument();
			xmlDoc.Load(xmlStream);
		}

		public List<string> parseHealthcareLocations()
		{
			XmlNodeList nodes = xmlDoc.DocumentElement.SelectNodes("//PlaceSearchResponse/result");
			List<string> locations = new List<string>();
			foreach(XmlNode node in nodes)
			{
				string data = "";
				data += node.SelectSingleNode("name").InnerText;
				data += " -- Located at: " + node.SelectSingleNode("vicinity").InnerText;
				locations.Add(data);
			}
			return locations;
		}

		public double getLatitude()
		{
			XmlNodeList nodes = xmlDoc.DocumentElement.SelectNodes("//GeocodeResponse/result/geometry/location");
			double latitude = -1;
			foreach (XmlNode node in nodes)
			{
				latitude = Convert.ToDouble(node.SelectSingleNode("lat").InnerText);
			}
			return latitude;
		}

		public double getLongitude()
		{
			XmlNodeList nodes = xmlDoc.DocumentElement.SelectNodes("//GeocodeResponse/result/geometry/location");
			double longitude = -1;
			foreach (XmlNode node in nodes)
			{
				longitude = Convert.ToDouble(node.SelectSingleNode("lng").InnerText);
			}
			return longitude;
		}
	}
}