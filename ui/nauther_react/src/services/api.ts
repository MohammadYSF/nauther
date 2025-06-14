import axios from 'axios';
export interface ApiError {
  message: string;
  status?: number;
  data?: any;
}
export const extractApiError = (error: unknown): ApiError => {
  if (axios.isAxiosError(error)) {
    return {
      message: error.message,
      status: error.response?.status,
      data: error.response?.data,
    };
  }

  return { message: 'Unexpected error', data: error };
};
const api = axios.create({
  baseURL: 'https://localhost:5001/api', // Change to your backend base URL
  headers: {
    'Content-Type': 'application/json',
  },
});
api.interceptors.response.use(
  res => res,
  err => {
    const apiError = extractApiError(err);
    // Optionally log to monitoring service here
    return Promise.reject(apiError); // unify error output
  }
);
// Request interceptor to add token
api.interceptors.request.use((config) => {
  const token =JSON.parse(sessionStorage.getItem("oidc.default")??"").tokens.accessToken??"";
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
  return error;
});
export default api;
