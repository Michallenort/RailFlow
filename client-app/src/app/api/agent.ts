import axios, { AxiosError, AxiosResponse } from "axios";
import { SignInFormValues, SignUpFormValues } from "../models/user";
import { store } from "../stores/store";
import { router } from "../router/Routes";
import { toast } from "react-toastify";
import { Station, StationFormValues } from "../models/station";
import { PaginatedResult } from "../models/pagination";

axios.defaults.baseURL = "https://localhost:44363";

const responseBody = (response: AxiosResponse) => response;

axios.interceptors.request.use(config => {
  const token = store.tokenStore.token;
  if (token && config.headers) config.headers.Authorization = `Bearer ${token}`;
  return config;
})

axios.interceptors.response.use(async response => {
  const {page, pageSize, totalCount, totalPages} = response.data;

  if (page && pageSize && totalCount && totalPages) {
    response.data = new PaginatedResult(response.data.items, {page, pageSize, totalCount, totalPages});
    return response as AxiosResponse<PaginatedResult<any>>;
  }

  return response;
}, (error: AxiosError) => {
  const { data, status, config } = error.response as AxiosResponse;
  return error.response;
})

const requests = {
  getPage: <T>(url: string, params: URLSearchParams) => axios.get<PaginatedResult<T>>(url, { params }).then(responseBody),
  get: <T>(url: string) => axios.get<T>(url).then(responseBody),
  post: (url: string, body: {}) => axios.post(url, body).then(responseBody),
  put: (url: string, body: {}) => axios.put(url, body).then(responseBody),
  delete: (url: string) => axios.delete(url).then(responseBody)
}

const Users = {
  signin: (user: SignInFormValues) => requests.post('/User/sign-in', user),
  signup: (user: SignUpFormValues) => requests.post('/User/sign-up', user),
  accountdetails: () => requests.get('/User/account-details')
}

const Stations = {
  list: (params: URLSearchParams) => requests.getPage<PaginatedResult<Station[]>>('/Station', params).then(responseBody),
  details: (id: string) => requests.get(`/Station/${id}`),
  create: (station: StationFormValues) => requests.post('/Station', station),
  update: (station: any) => requests.put(`/Station/${station.id}`, station),
  delete: (id: string) => requests.delete(`/Station/${id}`)
}

const agent = {
  Users,
  Stations
}

export default agent;