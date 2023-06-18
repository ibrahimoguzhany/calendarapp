import { API_ROUTES } from '../utils/constants';
import axios from 'axios';
export function storeTokenInLocalStorage(token) {
  localStorage.setItem('token', token);
}

export function getTokenFromLocalStorage() {
  return localStorage.getItem('token');
}

export async function getAuthenticatedUser() {
  const defaultReturnObject = { authenticated: false, user: null };
  try {
    const token = getTokenFromLocalStorage();
    if (!token) {
      return defaultReturnObject;
    }
    const response = await axios({
      method: 'GET',
      url: API_ROUTES.GET_USER,
      headers: {
        Authorization: `Bearer ${token}`
      }
    });
    console.log("+>>",response.data);
    var res = response.data;
    if(!!response.data) {
      return { authenticated: true, user: res.data };
    }
  }
  catch (err) {
    console.log('getAuthenticatedUser, Something Went Wrong', err);
    return defaultReturnObject;
  }
}
