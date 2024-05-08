using Infrastructure;
using Interface;
using Helper;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace CodePortfolio.Models
{
	/// <summary>
	/// 
	/// </summary>
	public class CustomViewModel : DBViewModel
	{
		[Required]
		[Display(Name = "ID")]
		public int id { get; set; }

		[Required]
		[Display(Name = "CustomerID")]
		public int customerId { get; set; }

		[Required]
		[Display(Name = "Title")]
		public string title { get; set; }

		[Required]
		[Display(Name = "NameLong")]
		public string refcode { get; set; }

		[Required]
		[Display(Name = "Visible")]
		public bool active { get; set; }

		[IgnoreDataMember]
		public string activeLabel { get; set; }

		[Required]
		[Display(Name = "ImageUser")]
		public string imageUser { get; set; }

		[IgnoreDataMember]
		public string imageUserFile{ get; set; }

		[IgnoreDataMember]
		public string imageUserFileName { get; set; }

		[Required]
		[Display(Name = "ImageCustomer")]
		public string imageGuest { get; set; }

		[Required]
		[Display(Name = "imageGuestOverride")]
		public string imageGuestOverride { get; set; }

		[IgnoreDataMember]
		public string imageGuestFile { get; set; }

		[IgnoreDataMember]
		public string imageGuestFileName { get; set; }

		[Required]
		[Display(Name = "Container1")]
		public int container1 { get; set; }

		[IgnoreDataMember]
		public string container1Name { get; set; }

		[Required]
		[Display(Name = "Container2")]
		public int? container2 { get; set; }

		[IgnoreDataMember]
		public string container2Name { get; set; }

		[Required]
		[Display(Name = "Container1")]
		public CustomContainer container1Object { get; set; }

		[Required]
		[Display(Name = "Container2")]
		public CustomContainer container2Object { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public partial class CustomContainer
		{
			[Required]
			[Display(Name = "ID")]
			public int id { get; set; }

			[Required]
			[Display(Name = "Type")]
			public int containerType { get; set; }

			[Required]
			[Display(Name = "Green")]
			public int green { get; set; }

			[Required]
			[Display(Name = "Red")]
			public int red { get; set; }

			[Required]
			[Display(Name = "Amber")]
			public int amber { get; set; }

			/// <summary>
			/// 
			/// </summary>
			public CustomContainer()
			{
				id = 0;
				containerType = 0;
				green = 0;
				red = 0;
				amber = 0;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public CustomViewModel()
		{
			id = 0;
			title = string.Empty;
			refcode = string.Empty;
			active = false;
			imageUser = string.Empty;
			imageGuest = string.Empty;
			imageGuestOverride = string.Empty;
			container1 = 0;
			container1Object = new CustomContainer();
			container2Object = new CustomContainer();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="db"></param>
		public CustomViewModel(IDb? db)
		{
			id = 0;
			title = string.Empty;
			refcode = string.Empty;
			active = false;
			imageUser = string.Empty;
			imageGuest = string.Empty;
			imageGuestOverride = string.Empty;
			container1Object = new CustomContainer();
			container2Object = new CustomContainer();
			_db = db;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="db"></param>
		/// <param name="customerId"></param>
		/// <param name="CustomId"></param>
		/// <returns></returns>
		public static List<CustomViewModel> GetCustoms(IDb db, int customerId, int CustomId = 0, bool showDeactivated = true)
		{
			if (_db == null) _db = db;

			CustomViewModel Custom = new CustomViewModel(_db);
			CustomContainer prodContainer1 = new CustomContainer();
			List<CustomViewModel> CustomList = new List<CustomViewModel>();

			List<SpParam> paramCustom = new List<SpParam>
			{
				new SpParam("i_customer_id", System.Data.SqlDbType.Int, 50, customerId),
				new SpParam("i_Custom_id", System.Data.SqlDbType.Int, 50, CustomId),
			};

			if (showDeactivated)
			{
				paramCustom.Add(new SpParam("i_show_deactivated", System.Data.SqlDbType.Int, 50, 0));
			}
			else 
			{
				paramCustom.Add(new SpParam("i_show_deactivated", System.Data.SqlDbType.Int, 50, 1));
			}

			if (CustomId == 0)
			{
				List<System.Data.DataTable> dbCustomList = _db.ExecuteSp("Custom_Get", paramCustom);
				if (dbCustomList.Count != 0)
				{
					CustomList = DbToModelMapper.MapDbTableToModel(dbCustomList.First(), Custom.GetType()).ConvertAll(x => (CustomViewModel)x);
				}
			}
			else
			{
				List<System.Data.DataTable> dbCustomList = _db.ExecuteSp("Custom_Get", paramCustom);
				Custom = (CustomViewModel)DbToModelMapper.MapDbTableToModel(dbCustomList.First(), Custom.GetType()).First();

				List<SpParam> paramProdContainer1 = new List<SpParam>
				{
					new SpParam("i_Custom_container_id", System.Data.SqlDbType.Int, 50, Custom.container1)
				};

				List<System.Data.DataTable> dbProdContainer1 = _db.ExecuteSp("Custom_CONTAINER_Get", paramProdContainer1);
				Custom.container1Object = (CustomContainer)DbToModelMapper.MapDbTableToModel(dbProdContainer1.First(), prodContainer1.GetType()).First();


				if (Custom.container2 != null)
				{
					List<SpParam> paramProdContainer2 = new List<SpParam>
				{
					new SpParam("i_Custom_container_id", System.Data.SqlDbType.Int, 50, (int) Custom.container2)
				};

					List<System.Data.DataTable> dbProdContainer2 = _db.ExecuteSp("Custom_CONTAINER_Get", paramProdContainer2);
					Custom.container2Object = (CustomContainer)DbToModelMapper.MapDbTableToModel(dbProdContainer2.First(), prodContainer1.GetType()).First();
				}

				CustomList.Add(Custom);
			}

			return CustomList;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="db"></param>
		/// <returns></returns>
		public static List<CustomContainer> GetCustomContainer(IDb? db, int prodContainerID = 0) 
		{
			List<SpParam> paramList = new List<SpParam>
				{
					new SpParam("i_Custom_container_id", System.Data.SqlDbType.Int, 50, prodContainerID)
				};
			List<System.Data.DataTable> dbContainerList = _db.ExecuteSp("Custom_CONTAINER_Get", paramList);
			List<CustomContainer> CustomContainer = DbToModelMapper.MapDbTableToModel(dbContainerList.First(), new CustomContainer().GetType()).ConvertAll(x => (CustomContainer)x);

			return CustomContainer;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="db"></param>
		/// <param name="customerId"></param>
		/// <param name="updateCustom"></param>
		/// <param name="secondContainer"></param>
		/// <returns></returns>
		public static CustomViewModel UpdateCustom(IDb db, int customerId, CustomViewModel updateCustom, bool secondContainer = false)
		{
			if (_db == null) _db = db;

			//preserve wysiwyg code
			string wysiwyg = updateCustom.imageGuestOverride;

			// Sanitize Inputs from Html and JS
			object toSanitize = updateCustom;
			updateCustom = (CustomViewModel)Sanitizer.Sanitize(toSanitize);

			// set preserved wysiwyg
			updateCustom.imageGuestOverride = wysiwyg;

			List<SpParam> parameters = new List<SpParam>
			{
				new SpParam("i_customer_id", System.Data.SqlDbType.Int, 50, customerId),
				new SpParam("i_Custom_id", System.Data.SqlDbType.Int, 50, updateCustom.id),
				new SpParam("i_image_user", System.Data.SqlDbType.NVarChar, 255, updateCustom.imageUser),
				new SpParam("i_image_guest", System.Data.SqlDbType.NVarChar, 255, updateCustom.imageGuest),
				new SpParam("i_image_guest_override", System.Data.SqlDbType.NVarChar, 1000000, updateCustom.imageGuestOverride),
				new SpParam("i_refcode", System.Data.SqlDbType.NVarChar, 255, updateCustom.refcode),
				new SpParam("i_title", System.Data.SqlDbType.NVarChar, 255, updateCustom.title),
				new SpParam("i_active", System.Data.SqlDbType.Bit, 1, updateCustom.active)
			};
			List<SpParam> container1 = new List<SpParam>
			{
				new SpParam("i_container1_id", System.Data.SqlDbType.Int, 50, updateCustom.container1),
				new SpParam("i_container1_type", System.Data.SqlDbType.Int, 50, updateCustom.container1Object.containerType),
				new SpParam("i_container1_red", System.Data.SqlDbType.Int, 50, updateCustom.container1Object.red),
				new SpParam("i_container1_green", System.Data.SqlDbType.Int, 50, updateCustom.container1Object.green),
				new SpParam("i_container1_amber", System.Data.SqlDbType.Int, 50, updateCustom.container1Object.amber)
			};

			parameters.AddRange(container1);

			if (updateCustom.container2 != null)
			{
				List<SpParam> container2 = new List<SpParam>
				{
				new SpParam("i_container2_id", System.Data.SqlDbType.Int, 50, (int)updateCustom.container2),
				new SpParam("i_container2_type", System.Data.SqlDbType.Int, 50, updateCustom.container2Object.containerType),
				new SpParam("i_container2_red", System.Data.SqlDbType.Int, 50, updateCustom.container2Object.red),
				new SpParam("i_container2_green", System.Data.SqlDbType.Int, 50, updateCustom.container2Object.green),
				new SpParam("i_container2_amber", System.Data.SqlDbType.Int, 50, updateCustom.container2Object.amber)
				};

				parameters.AddRange(container2);
			}

			List<System.Data.DataTable> dbReturn = _db.ExecuteSp("Custom_Set", parameters, true);
			CustomViewModel Custom = (CustomViewModel)DbToModelMapper.MapDbTableToModel(dbReturn.First(), new CustomViewModel().GetType()).First();

			return Custom;
		}

		public static List<ProgramViewModel> UsedInPrograms(IDb db, int CustomId) {
			if (_db == null) _db = db;
			
			List<SpParam> parameters = new List<SpParam>() { new SpParam("i_Custom_id", System.Data.SqlDbType.Int, 50, CustomId) };
			List<System.Data.DataTable> dbReturn = _db.ExecuteSp("Custom_in_PROGRAM", parameters);
			List<ProgramViewModel> programs = new List<ProgramViewModel>();
			if (dbReturn.Count != 0) 
			{
				programs.AddRange(DbToModelMapper.MapDbTableToModel(dbReturn.First(), new ProgramViewModel().GetType()).ConvertAll(x => (ProgramViewModel)x));
			}

			return programs;
		}

		public static void DeleteCustom(IDb db, int customerId, int CustomId)
		{
			if (_db == null) _db = db;

			CustomViewModel Custom = GetCustoms(db, customerId, CustomId: CustomId).First();

			if (File.Exists("wwwroot/" + Custom.imageGuest)) 
			{
				File.Delete("wwwroot/" + Custom.imageGuest);
			}
			if (File.Exists("wwwroot/" + Custom.imageUser))
			{
				File.Delete("wwwroot/" + Custom.imageUser);
			}

			List<SpParam> paramCustom = new List<SpParam>
			{
				new SpParam("i_customer_id", System.Data.SqlDbType.Int, 50, customerId),
				new SpParam("i_Custom_id", System.Data.SqlDbType.Int, 50, CustomId),
			};
			_db.ExecuteSp("Custom_Delete", paramCustom, true);
		}
	}
}
