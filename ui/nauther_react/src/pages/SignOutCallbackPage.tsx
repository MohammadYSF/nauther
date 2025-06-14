import { Result, Button } from 'antd';
import { useNavigate } from 'react-router-dom';
import { LoginOutlined } from '@ant-design/icons';

export const SignOutSuccessPage = () => {
  const navigate = useNavigate();

  const handleReturnToLogin = () => {
    navigate('/'); 
  };

  return (
    <Result
      status="success"
      title="شما با موفقیت خارج شدید"
      subTitle="برای ورود دوباره می‌توانید از دکمه زیر استفاده کنید."
      extra={
        <Button
          type="primary"
          icon={<LoginOutlined />}
          onClick={handleReturnToLogin}
        >
          ورود مجدد
        </Button>
      }
    />
  );
};
