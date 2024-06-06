import React, { useContext } from "react";
import { AuthContext } from "../contexts/AuthContext";
import { Link } from "react-router-dom";
import { Menu } from "antd";
import { FaCartPlus } from "react-icons/fa";
import DropDownProfile from "./DropDownProfile";

const Navbar = () => {
  const { isAuthen, auth } = useContext(AuthContext);
  const role = auth?.role;

  const userItems = [
    {
      key: 1,
      label: (
        <Link to="/home" className="">
          Home
        </Link>
      ),
    },
    {
      key: 2,
      label: <Link to="/books/top10news">Top 10 New</Link>,
    },
    {
      key: 3,
      label: <Link to="/books/top10loved">Top 10 Loved</Link>,
    },
    {
      key: 4,
      label: (
        <Link to="/borrowing" className="">
          Borrowing
        </Link>
      ),
    },
  ];

  const guestItems = [
    {
      key: 1,
      label: (
        <Link to="/home" className="">
          Home
        </Link>
      ),
    },
    {
      key: 2,
      label: <Link to="/books/top10news">Top 10 New</Link>,
    },
    {
      key: 3,
      label: <Link to="/books/top10loved">Top 10 Loved</Link>,
    },
  ];

  const adminItems = [
    {
      key: 1,
      label: (
        <Link to="/admin/book" className="">
          Book
        </Link>
      ),
    },
    {
      key: 2,
      label: (
        <Link to="/admin/category" className="">
          Category
        </Link>
      ),
    },
    {
      key: 3,
      label: (
        <Link to="/admin/borrowing" className="">
          Borrowing
        </Link>
      ),
    },
    {
      key: 4,
      label: (
        <Link to="/admin/borrowing-extend" className="">
          Borrowing Extend
        </Link>
      ),
    },
    // {
    //   key: 5,
    //   label: (
    //     <Link to="/users" className="">
    //       User
    //     </Link>
    //   ),
    // },
  ];

  return (
    <nav className="bg-gray-200 px-10 py-1 text-black flex justify-between items-center sticky top-0 z-10">
      <div>
        <Link to="/" className="">
          <img src="/logo.png" className="w-16 h-16 inline" alt="logo" />
        </Link>
      </div>
      <Menu
        mode="horizontal"
        defaultSelectedKeys={["1"]}
        items={
          role === "user"
            ? userItems
            : role === "admin"
            ? adminItems
            : guestItems
        }
        className="bg-gray-200 text-lg flex justify-center border-b-0"
        style={{ flex: 1, minWidth: 0 }}
      />
      <div className="flex text-lg gap-6">
        {isAuthen && role === "user" && (
          <Link to="/cart" className="">
            <FaCartPlus className="inline mb-1" /> Cart
          </Link>
        )}
        {isAuthen ? (
          <DropDownProfile />
        ) : (
          <>
            <Link to="/login" className="">
              Login
            </Link>
            <Link to="/sign-up" className="">
              Sign up
            </Link>
          </>
        )}
      </div>
    </nav>
  );
};

export default Navbar;
