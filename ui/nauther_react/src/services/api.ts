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
  baseURL: '', // Change to your backend base URL
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
export default api;
