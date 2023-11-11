export interface Pagination {
  page: number;
  pageSize: number;
  totalCount: number;
  totalPages: number;
}

export class PaginatedResult<T> {
  items: T;
  pagination: Pagination;

  constructor(items: T, pagination: Pagination) {
    this.items = items;
    this.pagination = pagination;
  }
}

export class PagingParams {
  page: number;
  pageSize: number;

  constructor(page = 1, pageSize = 10) {
    this.page = page;
    this.pageSize = pageSize;
  }
}