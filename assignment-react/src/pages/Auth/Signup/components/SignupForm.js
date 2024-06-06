import React, { useState } from "react";
import { Form, Input, Button, message, Select } from "antd";
import { useNavigate } from "react-router-dom";
import axiosInstance from "../../../../utils/axiosInstance";

const SignupForm = () => {
  const navigate = useNavigate();
  const [loading, setLoading] = useState(false);

  const onFinish = async (values) => {
    setLoading(true);
    try {
      const response = await axiosInstance.post("/auths/register", values);
      const email = response.data.data.email;
      await axiosInstance.get(`/auths/request-active-account/${email}`);
      message.success(response.data.message);

      navigate("/login");
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
      <h1 className="text-2xl mb-4">Sign Up</h1>
      <Form onFinish={onFinish} layout="vertical">
        <Form.Item
          name="email"
          label="Email"
          rules={[
            { required: true, message: "Please input your email!" },
            { type: "email", message: "Please enter a valid email!" },
          ]}
        >
          <Input />
        </Form.Item>
        <Form.Item
          name="name"
          label="Name"
          rules={[{ required: true, message: "Please input your name!" }]}
        >
          <Input />
        </Form.Item>
        <Form.Item
          name="password"
          label="Password"
          rules={[
            { required: true, message: "Please input your password!" },
            {
              pattern: /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$/,
              message:
                "Password must be at least 8 characters long and contain at least one letter and one number",
            },
          ]}
        >
          <Input.Password />
        </Form.Item>
        <Form.Item
          name="confirmPassword"
          label="Confirm Password"
          dependencies={["password"]}
          rules={[
            { required: true, message: "Please confirm your password!" },
            ({ getFieldValue }) => ({
              validator(_, value) {
                if (!value || getFieldValue("password") === value) {
                  return Promise.resolve();
                }
                return Promise.reject(
                  new Error("The two passwords do not match!")
                );
              },
            }),
          ]}
        >
          <Input.Password />
        </Form.Item>
        <Form.Item name="roleId" hidden initialValue={2}>
          <Input />
        </Form.Item>
        <Form.Item>
          <Button type="primary" htmlType="submit" loading={loading}>
            Sign Up
          </Button>
        </Form.Item>
      </Form>
    </>
  );
};

export default SignupForm;
