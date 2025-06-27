import { OidcSecure } from "@axa-fr/react-oidc";
import { Outlet, useLocation } from "react-router-dom";

const PrivateRoute: React.FC = () => {
  // const { accessToken } = useAuth();
  const location = useLocation();

  // if (!accessToken) {
  //   // Redirect to login, preserving the current location
  //   return <Navigate to="/login" state={{ from: location }} replace />;
  // }

  return (
    <OidcSecure>
      <Outlet />
    </OidcSecure>);
};

export { PrivateRoute };