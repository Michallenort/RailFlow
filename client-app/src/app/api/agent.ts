import axios, { AxiosError, AxiosResponse } from "axios";
import { SignInFormValues, SignUpFormValues } from "../models/user";
import { store } from "../stores/store";
import { router } from "../router/Routes";
import { toast } from "react-toastify";

axios.defaults.baseURL = "https://localhost:44363";

const responseBody = (response: AxiosResponse) => response.data;

axios.interceptors.request.use(config => {
  const token = store.tokenStore.token;
  if (token && config.headers) config.headers.Authorization = `Bearer ${token}`;
  return config;
})

axios.interceptors.response.use(async response => {
  return response;
}, (error: AxiosError) => {
  const { data, status, config } = error.response as AxiosResponse;
  switch (status) {
      case 400:
          if (config.method === 'get' && data.errors.hasOwnProperty('id')) {
              router.navigate('/not-found');
          }
          if (data.errors) {
              const modalStateErrors = [];
              for (const key in data.errors) {
                  if (data.errors[key]) {
                      modalStateErrors.push(data.errors[key])
                  }
              }
              throw modalStateErrors.flat();
          } else {
              toast.error(data);
          }
          break;
      case 401:
          break;
      case 403:
          break;
      case 404:
          break;
      case 500:
          break;
  }
  return Promise.reject(error);
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

const agent = {
  Users
}

export default agent;