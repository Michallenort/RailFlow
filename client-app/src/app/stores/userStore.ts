import { makeAutoObservable, runInAction, values } from "mobx";
import { CreateUserFormValues, SignInFormValues, SignUpFormValues, User } from "../models/user";
import agent from "../api/agent";
import { store } from "./store";
import { router } from "../router/Routes";
import { getLocalStorageWithExpiry, removeLocalStorage, setLocalStorageWithExpiry } from "./localStorageHandler";
import { Pagination, PagingParams } from "../models/pagination";

export default class UserStore {
  loggedUser: User | null = null;
  users = new Map<string, User>();
  isLoading = false;
  pagination: Pagination | null = null;
  pagingParams = new PagingParams();
  searchTerm: string | null = null;

  constructor() {
    makeAutoObservable(this);

    this.loadUserDetails();  
  }

  get isLoggedIn() {
    return !!this.loggedUser;
  }

  get isAdmin() {
    return this.loggedUser?.roleName === 'Supervisor';
  }

  get isEmployee() {
    return this.loggedUser?.roleName === 'Employee';
  }

  loadUserDetails() {
    const userJson = getLocalStorageWithExpiry('user');
    if (userJson) {
      try {
        const userData = JSON.parse(userJson);
        userData.dateOfBirth = new Date(userData.dateOfBirth);
        runInAction(() => {
          this.loggedUser = userData;
        });
      } catch (error) {
        console.log(error);
      }
    }
  }

  saveUserDetails(user: User) {
    try {
      const userJson = JSON.stringify(user);
      setLocalStorageWithExpiry('user', userJson, 1);
    } catch (error) {
      console.log(error);
    }
  }

  signIn = async (values: SignInFormValues) => {
    try {
      const token = await agent.Users.signin(values);
      store.tokenStore.setToken(token.data.accessToken);
      const user = await agent.Users.accountdetails();
      runInAction(() => {
        this.loggedUser = user.data;
        this.saveUserDetails(user.data);
      });
    } catch (error) {
      throw error;
    }
  }

  signUp = async (values: SignUpFormValues) => {
    try {
      const response = await agent.Users.signup(values);
      return response;
    } catch (error) {
      throw error;
    }
  }

  logout = () => {
    store.tokenStore.setToken(null);
    this.loggedUser = null;
    removeLocalStorage('user');
    router.navigate('/');
  }

  setPagingParams = (pagingParams: PagingParams) => {
    this.pagingParams = pagingParams;
  }

  setSearchTerm = (searchTerm: string | null) => {
    this.searchTerm = searchTerm;
  }

  setPagination = (pagination: Pagination) => {
    this.pagination = pagination;
  }

  clearUsers = () => {
    this.users.clear();
  }

  get axiosParams() {
    const params = new URLSearchParams();

    if (this.searchTerm) {
      params.append('searchTerm', this.searchTerm);
    }

    params.append('page', this.pagingParams.page.toString());
    params.append('pageSize', this.pagingParams.pageSize.toString());
    return params;
  }

  private setUser = (user: User) => {
    this.users.set(user.id, user);
  }

  loadUsers = async () => {
    this.isLoading = true;
    this.clearUsers();

    try {
      const result = await agent.Users.list(this.axiosParams);
      result.data.items.forEach((user: User) => {
        this.setUser(user);
      });
      this.setPagination(result.data.pagination);
      this.isLoading = false;
    } catch(error) {
      console.log(error);
      this.isLoading = false;
    }
  }

  createUser = async (user: CreateUserFormValues) => {
    try {
      const response = await agent.Users.create(user);
      const newUser = new User(user);
      this.setUser(newUser);

      return response;
    } catch(error) {
      console.log(error);
    }
  }

  deleteUser = async (id: string) => {
    this.isLoading = true;
    try {
      await agent.Users.delete(id);
      runInAction(() => {
        this.users.delete(id);
        this.isLoading = false;
      })
    } catch(error) {
      console.log(error);
      this.isLoading = false;
    }
  }  
}