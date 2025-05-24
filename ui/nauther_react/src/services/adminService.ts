import api from './api';
type GetAdminByIdResponseDataModel = {
  id: string,
  name: string,
  roles: { id: string, name: string,displayName:string }[],
  permissions: { id: string, name: string,displayName:string }[]
};
type CreateAdminPayload = {
  name: string,
  roles: string[],
  permissions: string[],
  password: string,
  confirmPassword: string
}
type EditAdminPayload = {
  name: string,
  roles: string[],
  permissions: string[],
  password?: string,
  confirmPassword?: string
}
type CreateAdminResponseDataModel = {
  statusCode: number;
  message: string;
  validationErrors: any;
  data: {
    id: string
  }
};

type EditAdminResponseDataModel = {
  id: string
};
type GetAdminsResponseDataModel = {
  total: number,
  data: {
    id: string,
    name: string,
    roles: { id: string, name: string,displayName:string }[],
    permissions: { id: string, name: string ,displayName:string}[]
  }[]
}
export const getAdmins = async (
  page: number = 1,
  pageSize: number = 10,
  search: string = ''
): Promise<GetAdminsResponseDataModel> => {
  var res = await api.get<GetAdminsResponseDataModel>('https://mocki.io/v1/93df223d-3b7c-406c-adca-ae747117119d', {
    params: {
      page,
      pageSize,
      search,
    },
  });
  return res.data;
}

export const getAdminById = async (id: string): Promise<GetAdminByIdResponseDataModel> => {
  let res = await api.get<GetAdminByIdResponseDataModel>(`/admins/${id}`);
  return res.data;
}
export const createAdmin = async (data: CreateAdminPayload): Promise<CreateAdminResponseDataModel> => {
  let res = await api.post<CreateAdminResponseDataModel>('/user/register', data);
  return res.data;
}
export const editAdmin = async (id: string, data: EditAdminPayload): Promise<EditAdminResponseDataModel> => {
  let res = await api.put<EditAdminResponseDataModel>(`/admins/${id}`, data);
  return res.data;
} 