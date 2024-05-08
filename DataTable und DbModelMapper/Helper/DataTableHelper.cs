using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System.Reflection;
using System.Runtime.Serialization;

namespace CodePortfolio.Helper
{
	/// <summary>
	/// 
	/// </summary>
	public class DataTableHelper : Controller
	{
		private StringValues _draw;
		private StringValues _start;
		private StringValues _length;
		private StringValues _order;
		private StringValues _sortColumn;
		private StringValues _sortColumnDir;
		private StringValues _searchValue;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="request"></param>
		/// <param name="data"></param>
		/// <param name="searchBy"></param>
		/// <returns></returns>
		public static JsonResult Filter(HttpRequest request, IEnumerable<object> data, List<string>? searchByFields = null, Dictionary<int, string>? facetFilter = null)
		{
			try
			{
				

				request.Form.TryGetValue("draw", out StringValues _draw);
				request.Form.TryGetValue("start", out StringValues _start);
				request.Form.TryGetValue("length", out StringValues _length);
				request.Form.TryGetValue("order[0][column]", out StringValues _order);
				request.Form.TryGetValue("columns[" + _order + "][data]", out StringValues _sortColumn);
				request.Form.TryGetValue("order[0][dir]", out StringValues _sortColumnDir);
				request.Form.TryGetValue("search[value]", out StringValues _searchValue);

				if (facetFilter != null)
				{
					List<object> filteredData = new();
					foreach (KeyValuePair<int, string> pair in facetFilter) {
						request.Form.TryGetValue("columns[" + pair.Key + "][search][value]", out StringValues _columnSearch);

							string search = _columnSearch.FirstOrDefault();
							filteredData.AddRange(data.Where(d => $"{d.GetType().GetProperty(pair.Value)?.GetValue(d)}".Contains(search)).Except(filteredData));
						
					}
					data = filteredData;
				}
				

				//Paging Size (10,20,50,100)  
				int pageSize = _length.ToString() == null ? 0 : Convert.ToInt32(_length);
				int skip = _start.ToString() == null ? 0 : Convert.ToInt32(_start);
				int recordsTotal = 0;

				//Sorting  
				if (!(string.IsNullOrEmpty(_sortColumn) && string.IsNullOrEmpty(_sortColumnDir)))
				{
					switch (_sortColumnDir.ToString())
					{
						case "asc":
							data = data.OrderBy(s => s.GetType().GetProperty(_sortColumn.ToString())?.GetValue(s)).ToList();
							break;
						case "desc":
							data = data.OrderByDescending(s => s.GetType().GetProperty(_sortColumn.ToString())?.GetValue(s)).ToList();
							break;
						default:
							break;
					}
				}
				//Search  
				if (!string.IsNullOrEmpty(_searchValue) && data.Count() > 0)
				{
					string searchValue = ($"{_searchValue}" ?? "").ToLower();

					// No pre defined columns mean, all columns without IgnoreDataMemberAttribtue
					if (searchByFields == null || searchByFields.Count == 0)
					{
						searchByFields = new();
						foreach (PropertyInfo column in data.First().GetType().GetProperties().Where(p => !p.IsDefined(typeof(IgnoreDataMemberAttribute), false)))
						{
							searchByFields.Add(column.Name);
						}
					}

					// look for search value in all columns if found add to filteredData
					List<object> filteredData = new();
					foreach (string searchBy in searchByFields)
					{
						filteredData.AddRange(data.Where(d => $"{d.GetType().GetProperty(searchBy)?.GetValue(d)}".ToLower().Contains(searchValue)).Except(filteredData));
					}
					data = filteredData;
				}

				//total number of rows count   
				recordsTotal = data.Count();
				//Paging   
				data = data.Skip(skip).Take(pageSize).ToList();
				//Returning Json Data  
				JsonResult json = new JsonResult(new { draw = _draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
				return json;
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}
