const API_URL = 'https://localhost:44320'
export const API_ROUTES = {
  REGISTER: `${API_URL}/api/account/register`,
  LOGIN: `${API_URL}/api/account/login`,
  REFRESH: `${API_URL}/api/account/refresh`,
  LOGOUT: `${API_URL}/api/account/logout`,
  SIGNALR: `${API_URL}/remind`,

  ADD_ROLE: `${API_URL}/api/role/addrole`,
  GET_LIST_ROLE: `${API_URL}/api/role/getlistrole`,

  UPDATE_USER: `${API_URL}/api/user/updateuser`,
  UPDATE_PASSWORD: `${API_URL}/api/user/updatepassword`,
  GET_AUTH: `${API_URL}/api/user/getauth`,
  GET_USER: `${API_URL}/api/user/getuser`,
  
  ADD_EVENT: `${API_URL}/api/event/addevent`,
  GET_LIST_EVENT: `${API_URL}/api/event/getlistevent`,
  GET_EVENT_BY_ID: `${API_URL}/api/event/geteventbyid`,
  UPDATE_EVENT: `${API_URL}/api/Event/UpdateEvent`,
  DELETE_EVENT: `${API_URL}/api/event/deleteevent`,

}

export const APP_ROUTES = {
  SIGN_UP: '/signup',
  SIGN_IN: '/signin',
  CALENDAR: '/calendar',
}