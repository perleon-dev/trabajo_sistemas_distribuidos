using System.Collections.Generic;

namespace PreContracts.Api.Application.Queries.ViewModels
{
    public class PaginationViewModel<T>
    {
        public PaginationViewModel(BasePagination pagination, IEnumerable<T> items)
        {
            this.pagination = pagination;
            this.items = items;
        }

        public BasePagination pagination { get; }
        public IEnumerable<T> items { get; }
    }

    public class PaginationRequest
    {
        public int pageIndex { get; set; }
        public int pageSize { get; set; }
        public string sort { get; set; }

        BasePagination _pagination;
        public BasePagination pagination
        {
            get
            {
                if (this._pagination == null)
                {
                    if (this.pageIndex > 0 && this.pageSize > 0)
                        this._pagination = new BasePagination(this.pageIndex, this.pageSize, this.sort);
                    else
                        this._pagination = new BasePagination(this.sort);
                }

                return this._pagination;
            }
            set
            {
                this._pagination = value;
            }
        }
    }

    public class BasePagination
    {
        public BasePagination(string sort)
        {
            this.sort = (sort == null) ? "" : sort;
        }

        public BasePagination(int pageIndex, int pageSize, string sort)
        {
            this.pageIndex = pageIndex;
            this.pageSize = pageSize;
            this.sort = (sort == null) ? "" : sort;
        }

        public int pageIndex { get; set; }
        public int pageSize { get; set; }
        public int pageCount { get; set; }
        public string sort { get; set; }

        private int _total = 0;
        public int total
        {
            get { return this._total; }
            set
            {
                if (this.pageSize > 0)
                    this.pageCount = (value / this.pageSize + ((value % this.pageSize == 0) ? 0 : 1));
                else
                    this.pageCount = 1;

                this._total = value;
            }
        }
    }
}
