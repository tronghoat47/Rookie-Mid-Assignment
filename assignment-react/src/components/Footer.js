import React from "react";
import { Link } from "react-router-dom";
import { FaFacebook, FaTwitter, FaInstagram, FaLinkedin } from "react-icons/fa";

const Footer = () => {
  return (
    <footer className="bg-gray-600 text-white p-6 bottom-0 w-full mt-10">
      <div className="container mx-auto flex justify-between items-center flex-wrap">
        <div className="mb-4 md:mb-0">
          <Link to="/" className="text-2xl font-bold">
            Your Library
          </Link>
          <p className="mt-2 text-sm">
            &copy; 2024 Your Library. All rights reserved.
          </p>
        </div>
        <div className="mb-4 md:mb-0">
          <h3 className="text-lg font-semibold">Stay Connected</h3>
          <div className="flex gap-4 mt-2">
            <a
              href="https://facebook.com/yourlibrary"
              className="hover:text-gray-400"
              target="_blank"
              rel="noopener noreferrer"
            >
              <FaFacebook size={24} />
            </a>
            <a
              href="https://twitter.com/yourlibrary"
              className="hover:text-gray-400"
              target="_blank"
              rel="noopener noreferrer"
            >
              <FaTwitter size={24} />
            </a>
            <a
              href="https://instagram.com/yourlibrary"
              className="hover:text-gray-400"
              target="_blank"
              rel="noopener noreferrer"
            >
              <FaInstagram size={24} />
            </a>
            <a
              href="https://linkedin.com/company/yourlibrary"
              className="hover:text-gray-400"
              target="_blank"
              rel="noopener noreferrer"
            >
              <FaLinkedin size={24} />
            </a>
          </div>
        </div>
        <div className="mb-4 md:mb-0">
          <h3 className="text-lg font-semibold">Quick Links</h3>
          <div className="flex gap-4 mt-2">
            <Link to="/about" className="hover:text-gray-400">
              About
            </Link>
            <Link to="/contact" className="hover:text-gray-400">
              Contact
            </Link>
            <Link to="/terms" className="hover:text-gray-400">
              Terms of Service
            </Link>
            <Link to="/privacy" className="hover:text-gray-400">
              Privacy Policy
            </Link>
          </div>
        </div>
        <div>
          <h3 className="text-lg font-semibold">Newsletter</h3>
          <p className="text-sm mt-2">
            Subscribe to our newsletter for the latest updates.
          </p>
          <form className="flex mt-2">
            <input
              type="email"
              className="px-2 py-1 rounded-l bg-gray-700 text-white border border-gray-600"
              placeholder="Your email"
            />
            <button className="px-3 py-1 bg-blue-500 text-white rounded-r hover:bg-blue-600">
              Subscribe
            </button>
          </form>
        </div>
      </div>
    </footer>
  );
};

export default Footer;
