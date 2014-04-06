using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Young.CustomTable;
using Young.Web.Models.Command.YoungTable;
using Young.Web.Models.EasyUIView;

namespace Young.Web.Controllers
{
    public class APIDynamicDataController : ApiController
    {
        //
        // GET: /APIDynamicData/

        public DataGridModel GetDataList([FromUri]QueryDataCommand command)
        {
            if (command.IsPaging)
            {
                var table = CustomTableTools.Query(command.TableCode, command.Page - 1, command.Rows, command.Columns);
                DataGridModel model = new DataGridModel(table);
                model.total = CustomTableTools.QueryCount(command.TableCode);
                return model;
            }
            else
            {
                return new DataGridModel(CustomTableTools.Query(command.TableCode, command.Columns));
            }
        }

        public HttpResponseMessage DeleteData(DeleteDataCommand command)
        {
            CustomTableTools.DeleteData(command.TableCode, command.DataID);
            return new HttpResponseMessage { StatusCode = System.Net.HttpStatusCode.OK };
        }

    }
}
