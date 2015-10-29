using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using OutPanApiGet;

namespace OutPanGet_Web
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var cred = GetCredentials();
                if (!string.IsNullOrEmpty(cred))
                {
                    txtApiValue.Value = cred;
                }
            }
        }

        public void btnGet_Click(object sender, EventArgs e)
        {
            var credentials = GetCredentials();
            if (string.IsNullOrEmpty(credentials))
            {
                HttpContext.Current.Response.Write("<SCRIPT LANGUAGE='JavaScript'>alert('Please set your credentials first!')</SCRIPT>");
                return;
            }

            var ean = GetEan();
            if (string.IsNullOrEmpty(ean))
            {
                HttpContext.Current.Response.Write("<SCRIPT LANGUAGE='JavaScript'>alert('Please set a EAN first!')</SCRIPT>");
                return;
            }

            var suffix = GetSuffix();

            var url = SetUrl(ean, suffix);

            WebRequest myReq = WebRequest.Create(url);
            myReq.Headers["Authorization"] = "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes(credentials));
            WebResponse wr = myReq.GetResponse();
            Stream receiveStream = wr.GetResponseStream();
            if (receiveStream != null)
            {
                StreamReader reader = new StreamReader(receiveStream, Encoding.UTF8);
                string content = reader.ReadToEnd();
                switch (suffix)
                {
                    case "":
                        var allResult = JsonConvert.DeserializeObject<OutpanApiAllModel>(content);
                        var formatted = JsonConvert.SerializeObject(allResult, Formatting.Indented);
                        ClearText();
                        SetTextOutPut(formatted);
                        break;
                    case "attributes":
                        var attrResult = JsonConvert.DeserializeObject<OutpanApiAttributesModel>(content);
                        var attrformatted = JsonConvert.SerializeObject(attrResult, Formatting.Indented);
                        ClearText();
                        SetTextOutPut(attrformatted);
                        break;
                    case "images":
                        var imageResult = JsonConvert.DeserializeObject<OutpanApiImageModel>(content);
                        var imgformatted = JsonConvert.SerializeObject(imageResult, Formatting.Indented);
                        ClearText();
                        SetTextOutPut(imgformatted);
                        break;
                    case "name":
                        var nameResult = JsonConvert.DeserializeObject<OutpanApiNameModel>(content);
                        var nameformatted = JsonConvert.SerializeObject(nameResult, Formatting.Indented);
                        ClearText();
                        SetTextOutPut(nameformatted);
                        break;
                    case "videos":
                        var videoResult = JsonConvert.DeserializeObject<OutpanApiVideosModel>(content);
                        var videoformatted = JsonConvert.SerializeObject(videoResult, Formatting.Indented);
                        ClearText();
                        SetTextOutPut(videoformatted);
                        break;
                }
            }
        }

        public string SetUrl(string gtin, string suffix)
        {
            var url = "https://api.outpan.com/v1/products/" + gtin + "/" + suffix;
            return url;
        }

        public string GetEan()
        {
            return txtEANNumber.Value.Trim();
        }

        public string GetSuffix()
        {
            if (cboRequestType.SelectedItem.ToString() == "All")
            {
                return "";
            }
            return cboRequestType.SelectedItem.ToString().ToLower();
        }

        public void SetCredetntials(string credentials)
        {
            Session["userCredentials"] = credentials;
        }

        public string GetCredentials()
        {
            var cred = Session["userCredentials"];
            if (cred != null)
            {
                return cred.ToString();
            }
            else
            {
                return string.Empty;
            }
        }

        public void btnSetCredentials_Click(object sender, EventArgs e)
        {
            var apikey = txtApiValue.Value;
            string validate = null;
            if (!string.IsNullOrEmpty(apikey))
            {
                validate = apikey.Substring(apikey.Length - 1, 1);
            }
            if (validate != ":")
            {
                HttpContext.Current.Response.Write("<SCRIPT LANGUAGE='JavaScript'>alert('Please Enter a valid Api Key in the following format:- XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX:')</SCRIPT>");
            }
            else
            {
                SetCredetntials(apikey);
            }
        }

        public void ClearText()
        {
            rTxtOutput.Value = string.Empty;
        }

        public void SetTextOutPut(string formatted)
        {
            rTxtOutput.Value = formatted;
        }
    }
}