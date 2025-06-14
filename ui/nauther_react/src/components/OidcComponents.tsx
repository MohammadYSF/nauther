
import { Spin, Typography,Result, Button } from 'antd';

export const LoadingComponent = () => (
  <div style={{ textAlign: 'center', marginTop: '20%' }}>
    <Spin size="large" />
    <Typography.Title level={4} style={{ marginTop: 20 }}>
      در حال بارگذاری...
    </Typography.Title>
  </div>
);

export const SessionLostComponent = () => (
  <Result
    status="warning"
    title="جلسه منقضی شده است"
    subTitle="لطفاً دوباره وارد شوید."
    extra={
      <Button type="primary" onClick={() => window.location.reload()}>
        ورود مجدد
      </Button>
    }
  />
);

export const AuthenticatingComponent = () => (
  <div style={{ textAlign: 'center', marginTop: '20%' }}>
    <Spin size="large" />
    <Typography.Title level={4} style={{ marginTop: 20 }}>
      در حال احراز هویت...
    </Typography.Title>
  </div>
);

export const AuthenticatingErrorComponent = () => (
  <Result
    status="error"
    title="خطا در احراز هویت"
    subTitle="مشکلی در روند ورود به حساب پیش آمده است."
    extra={
      <Button type="primary" onClick={() => window.location.reload()}>
        تلاش مجدد
      </Button>
    }
  />
);

export const CallbackSuccessComponent = () => (
  <Result
    status="success"
    title="ورود موفقیت‌آمیز بود"
    subTitle="در حال هدایت به داشبورد..."
  />
);