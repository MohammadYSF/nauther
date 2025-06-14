import axios from 'axios';
import type { ValidationError } from '../types/baseApiResponseModel';
export interface ApiError {
  message: string;
  status?: number;
  data?: any;
  validationErrors?:ValidationError[];
}
// export const extractApiError = (error: unknown): ApiError => {
//   if (axios.isAxiosError(error)) {
//     return {
//       message: error.message,
//       status: error.response?.status,
//       data: error.response?.data,
//       validationErrors:error.response?.data.validationErrors
//     };
//   }

//   return { message: 'Unexpected error', data: error };
// };
const api = axios.create({
  baseURL: 'https://localhost:5001/api', // Change to your backend base URL
  headers: {
    'Content-Type': 'application/json',
  },
  validateStatus: function (status) {
    // Only throw (i.e. reject) for these codes
    return [400, 401, 403, 404, 500,203,409].includes(status) === false;
  }
});
api.interceptors.response.use(
  res => res,
  // err => {
  //   const apiError = extractApiError(err);
  //   // Optionally log to monitoring service here
  //   return Promise.reject(apiError); // unify error output
  // }
);
// Request interceptor to add token
api.interceptors.request.use((config) => {
  const token = JSON.parse(sessionStorage.getItem("oidc.default") ?? "").tokens.accessToken ?? "";
  console.log("Token from sessionStorage:", token);
  // const token = localStorage.getItem('accesstoken');
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
}, (error) => {
  return Promise.reject(error);
});

// Response interceptor to handle errors
api.interceptors.response.use((response) => {
  return response;
}, (error) => {
  if (error.response?.status === 401) {
    localStorage.removeItem("accesstoken");
    window.location.href = '/login';
  }
  return Promise.reject(error);
});
export default api;
