const SignInOidc = () => {
  return (
    <div className="flex flex-col items-center justify-center h-screen bg-gray-100">
      <div className="bg-white p-8 rounded-lg shadow-md w-full max-w-md">
        <h2 className="text-2xl font-bold mb-6 text-center">Sign In with OIDC</h2>
        <p className="text-gray-600 mb-4 text-center">
          Redirecting to your identity provider...
        </p>
      </div>
    </div>
  );
}
export default SignInOidc;