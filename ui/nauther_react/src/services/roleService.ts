import api from './api';
type GetRoleByIdResponseDataModel = {
  statusCode: number;
  message: string;
  validationErrors: any;
  data: {
    id: string,
    name: string,
    displayName: string,
    permissions: { id: string, name: string, displayName: string }[]
  }
};
type GetRolesResponseDataModel = {
  total: number,
  data: {
    id: string,
    name: string,
    displayName: string,
    permissions: { id: string, name: string, displayName: string }[],
    users: { id: string, name: string }[],

  }[]
};
type CreateRolePayload = {
  name: string,
  displayName: string,
  permissionIds: string[]
};
type EditRolePayload = {
  name: string,
  displayName: string,
  permissionIds: string[]
};
type CreateRoleResponseModel = {
  statusCode: number;
  message: string;
  validationErrors: any;
  data: { id: string };
};
type EditRoleResponseModel = {
  id: string
};
export const getRoles = async (
  page: number = 1,
  pageSize: number = 10,
  search: string = ''
): Promise<GetRolesResponseDataModel> => {
  let res = await api.get<GetRolesResponseDataModel>('/role/all', {
    params: {
      page,
      pageSize,
      search,
    },
  });
  return res.data;

}

export const getRoleById = async (id: string): Promise<GetRoleByIdResponseDataModel> => {
  let res = await api.get<GetRoleByIdResponseDataModel>(`/role/${id}`);
  return res.data;
}

export const createRole = async (data: CreateRolePayload): Promise<CreateRoleResponseModel> => {
  let res = await api.post<CreateRoleResponseModel>('/role', data);
  return res.data;
}

export const editRole = async (id: string, data: EditRolePayload): Promise<EditRoleResponseModel> => {
  let res = await api.put<EditRoleResponseModel>(`/role/${id}`, data);
  return res.data;
}

export const updateRole = async (id: string, data: CreateRolePayload): Promise<CreateRoleResponseModel> => {
  let res = await api.put<CreateRoleResponseModel>(`/role/${id}`, data);
  return res.data;
};

export const deleteRole = async (id: string): Promise<void> => {
  await api.delete(`/roles/${id}`);
}; 