using Contracts.Api.Application.Queries.Interfaces;
using Contracts.Api.Application.Queries.Mappers;
using Contracts.Api.Application.Queries.Querys;
using Contracts.Api.Application.Queries.ViewModels;
using Contracts.Api.Domain.Util;
//using Contracts.Api.Services.Util;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Api.Application.Queries.Implementations
{
	public class PreContractQuery : IPreContractQuery
	{
		private readonly IQueryHandler _iGenericQuery;
		private readonly IPreContractMapper _iPreContractMapper;
		private readonly ICategoryQueryHandler _icategoryQueryHandler;

		public PreContractQuery(IQueryHandler iGenericQuery, IPreContractMapper iPreContractMapper, ICategoryQueryHandler icategoryQueryHandler)
		{
			_iGenericQuery = iGenericQuery ?? throw new ArgumentNullException(nameof(iGenericQuery));
			_iPreContractMapper = iPreContractMapper ?? throw new ArgumentNullException(nameof(iPreContractMapper));
			_icategoryQueryHandler = icategoryQueryHandler ?? throw new ArgumentNullException(nameof(icategoryQueryHandler));
		}

		public async Task<PreContractViewModel> GetById(int contract_id)
		{
			var parameters = new Dictionary<string, object>
			{
				{"contract_id", contract_id}
			};
			var parametersXml = await _iGenericQuery.BuildParametersXml(parameters);
			var result = await _iGenericQuery.Search<dynamic>(@"CONTRACT.adv_t_pre_contract_search", parametersXml, string.Empty);

			return result.Select(item => (PreContractViewModel)_iPreContractMapper.MapToPreContractViewModel(item)).FirstOrDefault();
		}

		public async Task<IEnumerable<PreContractViewModel>> GetBySearch(PreContractRequest request)
		{
			var parameters = new Dictionary<string, object>
			{
				{"contract_id", request.contract_id ?? 0},
				{"state", request.state ?? 0}
			};
			var parametersXml = await _iGenericQuery.BuildParametersXml(parameters);
			var result = await _iGenericQuery.Search<dynamic>(@"CONTRACT.adv_t_pre_contract_search", parametersXml, string.Empty);

			var items = result.Select(item => (PreContractViewModel)_iPreContractMapper.MapToPreContractViewModel(item));

			return items;
		}

		public async Task<(IEnumerable<PreContractViewModel>, int)> GetByFindAll(PreContractRequest request)
		{
			var parameters = new Dictionary<string, object>
			{
				{"contract_id", request.contract_id ?? 0},
				{"ruc", request.ruc ?? string.Empty},
				{"business_name", request.business_name ?? string.Empty},
				{"tradename", request.tradename ?? string.Empty},
				{"contract_start_date", request.contract_start_date ?? string.Empty},
				{"contract_end_date", request.contract_end_date ?? string.Empty},
				{"state", request.state ?? 0}
			};

			var parametersXml = await _iGenericQuery.BuildParametersXml(parameters);
			var (result, quantity) = await _iGenericQuery.FindAll<dynamic>(@"CONTRACT.adv_t_pre_contract_find_all", parametersXml, request.PageIndex, request.PageSize, (string.IsNullOrEmpty(request.SortProperty)) ? string.Empty: request.SortProperty);

			var items = result.Select(item => (PreContractViewModel)_iPreContractMapper.MapToPreContractViewModel(item));

			return (items, quantity);
		}

		public async Task<string> DownloadTemplateContractMarketPlace()
		{
			Console.WriteLine($"Iniciar");
			var assembly = Assembly.GetExecutingAssembly();
			var demo = assembly.GetManifestResourceNames();
			var resourceName = assembly.GetManifestResourceNames().First(s => s.EndsWith(TemplatePreContract.TemplateSellerCenter, StringComparison.CurrentCultureIgnoreCase));

			var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);
			var base64 = string.Empty;
			var list_category = await _icategoryQueryHandler.Search(new CategoryQuery { typeSeller =ContractTypePreContract.SellerCenter, sortProperty = "category_name" });
			List<DepartmentCategorySubCategory> ListDepartmentCategorySubCategory = new List<DepartmentCategorySubCategory>();
			ConcatenarCategory(ListDepartmentCategorySubCategory, list_category.ToList(), string.Empty, string.Empty);
			
			byte[] byte_;

			using (ExcelPackage package = new ExcelPackage(stream))
			{
				Console.WriteLine($"START ExcelPackage");

				var workSheet = package.Workbook.Worksheets["DATA MAESTRA"];
				var colorHeader = ColorTranslator.FromHtml("#2C7DCB");

				workSheet.Column(1).Width = 10;
				workSheet.Column(2).Width = 60;

				workSheet.Cells["A1:B1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
				workSheet.Cells["A1:B1"].Style.Fill.BackgroundColor.SetColor(colorHeader);
				workSheet.Cells["A1:B1"].Style.Font.Color.SetColor(Color.White);
				workSheet.Cells["A1:B1"].Style.Font.Bold = true;
				workSheet.Cells["A1:B1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
				workSheet.Cells["A1:B1"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

				workSheet.Row(1).Height = 20;
				
				workSheet.Cells["A1"].Value = "ID";
				workSheet.Cells["B1"].Value = "CATEGORIA";
				var index = 2;
				workSheet.Cells[$"A1:B1"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
				workSheet.Cells[$"A1:B1"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
				workSheet.Cells[$"A1:B1"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
				workSheet.Cells[$"A1:B1"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
				Console.WriteLine($"Crea la cabecera ExcelPackage");
				foreach (var item in ListDepartmentCategorySubCategory) 
				{
					try
					{
						workSheet.Cells[$"A{index}:B{index}"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
						workSheet.Cells[$"A{index}:B{index}"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
						workSheet.Cells[$"A{index}:B{index}"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
						workSheet.Cells[$"A{index}:B{index}"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

						workSheet.Cells[index, 1].Value = item.category_id;
						workSheet.Cells[index, 2].Value = $"{item.category_id} - {item.Description}";
						index++;
					}
					catch (Exception e)
					{
						Console.WriteLine(e.ToString());
					}
					
				}
				Console.WriteLine($"Termina de llenar la data ExcelPackage");
				var validationCell = workSheet.DataValidations.AddListValidation("D1");
				validationCell.Formula.ExcelFormula = $"=$B$2:$B${index-1}";


				byte_ = package.GetAsByteArray();
			}
			Console.WriteLine($"Manda los datos en formato BASE64");
			base64 = Convert.ToBase64String(byte_);
			return base64;
		}

		private void ConcatenarCategory(List<DepartmentCategorySubCategory> ListDepartmentCategorySubCategory, List<CategoryViewModel> list_category, string categoryParentCode, string base_) 
		{
			var listDepartmenst = from xCategory in list_category where xCategory.categoryParentCode == categoryParentCode select xCategory;

			if (listDepartmenst.Count() != 0) 
			{
				foreach (var dept in listDepartmenst)
				{
					var departmentCategorySubCategory = new DepartmentCategorySubCategory();

					departmentCategorySubCategory.category_id = dept.categoryId;
					departmentCategorySubCategory.Description = base_ + dept.categoryName;

					ListDepartmentCategorySubCategory.Add(departmentCategorySubCategory);
					ConcatenarCategory(ListDepartmentCategorySubCategory, list_category, dept.categoryCode, departmentCategorySubCategory.Description + "/");
				}
			}
		}

		public async Task<string> DownloadTemplateContractMarketPlaceVTex()
		{
			var assembly = Assembly.GetExecutingAssembly();
			var resourceName = assembly.GetManifestResourceNames().First(s => s.EndsWith(TemplatePreContract.TemplateVTex, StringComparison.CurrentCultureIgnoreCase));
			var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);
			var base64 = string.Empty;

			using (MemoryStream memory = new MemoryStream())
			{

				await stream.CopyToAsync(memory);
				base64 = Convert.ToBase64String(memory.ToArray());
			}

			return base64;
		}

		public async Task<string> DownloadTemplateContractMarketPlaceVTexToVTex() 
		{
			var assembly = Assembly.GetExecutingAssembly();
			var resourceName = assembly.GetManifestResourceNames().First(s => s.EndsWith(TemplatePreContract.TemplateVTexToVTex, StringComparison.CurrentCultureIgnoreCase));
			var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);
			var base64 = string.Empty;

			var list_category = await _icategoryQueryHandler.Search(new CategoryQuery { typeSeller = ContractTypePreContract.VTex, sortProperty = "category_name" });
			List<DepartmentCategorySubCategory> ListDepartmentCategorySubCategory = new List<DepartmentCategorySubCategory>();
			ConcatenarCategory(ListDepartmentCategorySubCategory, list_category.ToList(), string.Empty, string.Empty);

			byte[] byte_excel;

			using (ExcelPackage package = new ExcelPackage(stream))
			{
				var workSheet = package.Workbook.Worksheets["DATA MAESTRA"];
				var colorHeader = ColorTranslator.FromHtml("#2C7DCB");

				workSheet.Column(1).Width = 10;
				workSheet.Column(2).Width = 60;

				workSheet.Cells["A1:B1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
				workSheet.Cells["A1:B1"].Style.Fill.BackgroundColor.SetColor(colorHeader);
				workSheet.Cells["A1:B1"].Style.Font.Color.SetColor(Color.White);
				workSheet.Cells["A1:B1"].Style.Font.Bold = true;
				workSheet.Cells["A1:B1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
				workSheet.Cells["A1:B1"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

				workSheet.Row(1).Height = 20;

				workSheet.Cells["A1"].Value = "ID";
				workSheet.Cells["B1"].Value = "CATEGORIA";
				var index = 2;
				workSheet.Cells[$"A1:B1"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
				workSheet.Cells[$"A1:B1"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
				workSheet.Cells[$"A1:B1"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
				workSheet.Cells[$"A1:B1"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

				foreach (var item in ListDepartmentCategorySubCategory)
				{
					try
					{
						workSheet.Cells[$"A{index}:B{index}"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
						workSheet.Cells[$"A{index}:B{index}"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
						workSheet.Cells[$"A{index}:B{index}"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
						workSheet.Cells[$"A{index}:B{index}"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

						workSheet.Cells[index, 1].Value = item.category_id;
						workSheet.Cells[index, 2].Value = $"{item.category_id} - {item.Description}";
						index++;
					}
					catch (Exception e)
					{
						Console.WriteLine(e.ToString());
					}

				}
	
				var validationCell = workSheet.DataValidations.AddListValidation("D1");
				validationCell.Formula.ExcelFormula = $"=$B$2:$B${index - 1}";


				byte_excel = package.GetAsByteArray();
			}

			base64 = Convert.ToBase64String(byte_excel);
			return base64;
		}

	}
}
