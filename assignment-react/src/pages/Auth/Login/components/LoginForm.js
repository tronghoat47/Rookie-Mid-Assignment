import React, { useContext, useState } from "react";
import { Form, Input, Button, message } from "antd";
import { useNavigate } from "react-router-dom";
import axiosInstance from "../../../../utils/axiosInstance";
import { AuthContext } from "../../../../contexts/AuthContext";

const LoginForm = () => {
  const { login } = useContext(AuthContext);
  const navigate = useNavigate();
  const [loading, setLoading] = useState(false);
  const onFinish = async (values) => {
    setLoading(true);
    try {
      const response = await axiosInstance.post("/auths/login", values);
      login(response.data.data);
      if (response.data.data.role === "admin") {
        navigate("/admin");
      } else navigate("/home");
    } catch (error) {
      if (error.response && error.response.status === 409) {
        message.error(error.response.data);
      } else {
        message.error("Registration failed");
      }
    }
    setLoading(false);
  };
  return (
    <>
      <h1 className="text-2xl mb-4">Login</h1>
      <Form onFinish={onFinish} layout="vertical">
        <Form.Item
          name="email"
          label="Email"
          rules={[{ required: true, message: "Please input your email!" }]}
        >
          <Input />
        </Form.Item>
        <Form.Item
          name="password"
          label="Password"
          rules={[{ required: true, message: "Please input your password!" }]}
        >
          <Input.Password />
        </Form.Item>
        <Form.Item>
          <Button type="primary" htmlType="submit" loading={loading}>
            Login
          </Button>
        </Form.Item>
      </Form>
    </>
  );
};

export default LoginForm;
