using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliqueHR.Common.Models
{
    public class JqueryDatatableParamModel
    {

        //TODO 

        /// <summary>
        /// Gets or sets the information for DataTables to use for rendering.
        /// </summary>
        public int sEcho { get; set; }

        /// <summary>
        /// Gets or sets the display start point.
        /// </summary>
        public int iDisplayStart { get; set; }

        /// <summary>
        /// Gets or sets the number of records to display.
        /// </summary>
        public int iDisplayLength { get; set; }

        /// <summary>
        /// Gets or sets the Global search field.
        /// </summary>
        public string sSearch { get; set; }
        /// <summary>
        /// Gets or sets the Global search field.
        /// </summary>
        public string fromDate { get; set; }
        /// <summary>
        /// Gets or sets the Global search field.
        /// </summary>
        public string toDate { get; set; }
        /// <summary>
        /// Gets or sets the Global search field.
        /// </summary>
        public string fromTime { get; set; }
        /// <summary>
        /// Gets or sets the Global search field.
        /// </summary>
        public string toTime { get; set; }

        /// <summary>
        /// Gets or sets if the Global search is regex or not.
        /// </summary>
        public bool bRegex { get; set; }

        /// <summary>
        /// Gets or sets the number of columns being display (useful for getting individual column search info).
        /// </summary>
        public int iColumns { get; set; }

        /// <summary>
        /// Gets or sets indicator for if a column is flagged as sortable or not on the client-side.
        /// </summary>
        public ReadOnlyCollection<bool> bSortable_ { get; set; }

        /// <summary>
        /// Gets or sets indicator for if a column is flagged as searchable or not on the client-side.
        /// </summary>
        public ReadOnlyCollection<bool> bSearchable_ { get; set; }

        /// <summary>
        /// Gets or sets individual column filter.
        /// </summary>
        public ReadOnlyCollection<string> sSearch_ { get; set; }

        /// <summary>
        /// Gets or sets if individual column filter is regex or not.
        /// </summary>
        public ReadOnlyCollection<bool> bRegex_ { get; set; }

        /// <summary>
        /// Gets or sets the number of columns to sort on.
        /// </summary>
        public int? iSortingCols { get; set; }

        /// <summary>
        /// Gets or sets column being sorted on (you will need to decode this number for your database).
        /// </summary>
        public int iSortCol_0 { get; set; }
        public ReadOnlyCollection<int> iSortCol_ { get; set; }

        /// <summary>
        /// Gets or sets the direction to be sorted - "desc" or "asc".
        /// </summary>
        public string sSortDir_0 { get; set; }
        public ReadOnlyCollection<string> sSortDir_ { get; set; }

        /// <summary>
        /// Gets or sets the value specified by mDataProp for each column. 
        /// This can be useful for ensuring that the processing of data is independent 
        /// from the order of the columns.
        /// </summary>
        public ReadOnlyCollection<string> mDataProp_ { get; set; }

        /// <summary>
        /// Select type filter
        /// </summary>
        public string selectId { get; set; }

        /// <summary>
        /// select type 2 filter
        /// </summary>
        public string selectId2 { get; set; }

        /// <summary>
        /// select type 3 filter
        /// </summary>
        public string selectId3 { get; set; }

        public List<string> multiselectId { get; set; }

        /// <summary>
        /// for date filter
        /// </summary>
        public string selectDate { get; set; }

        /// <summary>
        /// date filter 2
        /// </summary>
        public string selectDate2 { get; set; }

        /// <summary>
        /// search by column filter
        /// </summary>
        public string searchByColumn { get; set; }

        public string id { get; set; }

        public string specialityid { get; set; }

        public string caseID { get; set; }

        public int userid { get; set; }
        public string specialities { get; set; }

        public string RoleName { get; set; }

        //public int ID { get; set; }

        public int hdnDisplayStart { get; set; }

        public int IsAssign { get; set; }

    }
}
