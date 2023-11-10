import axios, { AxiosError, AxiosResponse } from "axios";
import { SignInFormValues, SignUpFormValues } from "../models/user";
import { store } from "../stores/store";
import { router } from "../router/Routes";
import { toast } from "react-toastify";
import { StationFormValues } from "../models/station";

axios.defaults.baseURL = "https://localhost:44363";

const responseBody = (response: AxiosResponse) => response;

axios.interceptors.request.use(config => {
  const token = store.tokenStore.token;
  if (token && config.headers) config.headers.Authorization = `Bearer ${token}`;
  return config;
})

axios.interceptors.response.use(async response => {
  return response;
}, (error: AxiosError) => {
  const { data, status, config } = error.response as AxiosResponse;
  return error.response;
})

const requests = {
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
  list: () => requests.get('/Station'),
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