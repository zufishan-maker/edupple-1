// USA
export const locale = {
  lang: 'en',
  data: {
    TRANSLATOR: {
      SELECT: 'Select your language',
    },
    MENU: {
      NEW: 'new',
      ACTIONS: 'Actions',
      CREATE_POST: 'Create New Post',
      PAGES: 'Pages',
      FEATURES: 'Features',
      APPS: 'Apps',
      DASHBOARD: 'Dashboard',
      STUDENT: 'Students Management',
      TUTOR: 'Tutor Management'
    },
    BUTTONS: {
      CANCEL: 'Cancel',
      DELETE: 'Delete',
      CLOSE: 'Close'
    },
    AUTH: {
      GENERAL: {
        OR: 'Or',
        SUBMIT_BUTTON: 'Submit',
        CANCEL_BUTTON: 'Cancel',
        NO_ACCOUNT: 'Don\'t have an account?',
        SIGNUP_BUTTON: 'Sign Up',
        FORGOT_BUTTON: 'Forgot Password',
        BACK_BUTTON: 'Back',
        PRIVACY: 'Privacy',
        LEGAL: 'Legal',
        CONTACT: 'Contact',
      },
      LOGIN: {
        WELCOME: 'Welcome to Edupple',
        EMAIL: 'Email',
        PASSWORD: 'Password',
        FORGET_PASSWORD: 'Forget Password?',
        SIGN_IN: 'Sign In',
        TITLE: 'Login Account',
        BUTTON: 'Sign In',
      },
      FORGOT: {
        TITLE: 'Forgotten Password?',
        DESC: 'Enter your email to reset your password',
        SUCCESS: 'Your account has been successfully reset.'
      },
      RESET: {
        TITLE: 'Reset Password?',
        DESC: 'Enter details to reset your password',
        SUCCESS: 'Your account has been successfully reset.'
      },
      REGISTER: {
        TITLE: 'Sign Up',
        DESC: 'Enter your details to create your account',
        SUCCESS: 'Your account has been successfuly registered.'
      },
      INPUT: {
        EMAIL: 'Email',
        FULLNAME: 'Fullname',
        PASSWORD: 'Password',
        NEW_PASSWORD: 'New Password',
        CONFIRM_PASSWORD: 'Confirm Password',
        USERNAME: 'Username',
        OTP: 'OTP'
      },
      VALIDATION: {
        INVALID: '{{name}} is not valid',
        REQUIRED: '{{name}} is required',
        MIN_LENGTH: '{{name}} minimum length is {{min}}',
        AGREEMENT_REQUIRED: 'Accepting terms & conditions are required',
        NOT_FOUND: 'The requested {{name}} is not found',
        INVALID_LOGIN: 'The login detail is incorrect',
        REQUIRED_FIELD: 'Required field',
        MIN_LENGTH_FIELD: 'Minimum field length:',
        MAX_LENGTH_FIELD: 'Maximum field length:',
        INVALID_FIELD: 'Field is not valid',
      }
    },
    STUDENT: {
      COMMON: {
        SELECTED_RECORDS_COUNT: 'Selected records count: ',
        ALL: 'All',
        SUSPENDED: 'Suspended',
        ACTIVE: 'Active',
        FILTER: 'Filter',
        BY_STATUS: 'by Status',
        BY_TYPE: 'by Type',
        BUSINESS: 'Business',
        INDIVIDUAL: 'Individual',
        SEARCH: 'Search',
        IN_ALL_FIELDS: 'in all fields',
        DELETE_ALL: 'Delete All',
        FETCH_SELECTED: 'Fetch Selected',
        UPDATE_STATUS: 'Update Status'
      },
      STUDENTS: {
        STUDENTS: 'Students',
        STUDENTS_LIST: 'Students list',
        NEW_STUDENT: 'New Student',
        EDIT_STUDENT: 'Edit Student',
        CREATE_STUDENT: 'Create Student',
        TABLE: {
          COL_1: 'ID',
          COL_2: 'FIRSTNAME',
          COL_3: 'FIRSTNAME',
          COL_4: 'EMAIL',
          COL_5: 'GENDER',
          COL_6: 'STATUS',
          COL_7: 'TYPE',
          COL_8: 'ACTIONS',
        },
        FORM: {
          FULL_NAME: 'Full Name',
          UNIVERSITY_NAME: 'University Name',
          GENDER: 'Gender',
          DATE_OF_BIRTH: 'Date of birth',
          EMAIL: 'Email',
          PHONE_NUMBER: 'Phone Number',
          COUNTRY: 'Country',
          CITY: 'City',
          SUBJECTS: 'Subjects'
        },
        DELETE_STUDENT_SIMPLE: {
          TITLE: 'Student Delete',
          DESCRIPTION: 'Are you sure to permanently delete this student?',
          WAIT_DESCRIPTION: 'Student is deleting...',
          MESSAGE: 'Student has been deleted'
        },
        DELETE_STUDENT_MULTY: {
          TITLE: 'Students Delete',
          DESCRIPTION: 'Are you sure to permanently delete selected Students?',
          WAIT_DESCRIPTION: 'Students are deleting...',
          MESSAGE: 'Selected students have been deleted'
        },
        UPDATE_STATUS: {
          TITLE: 'Status has been updated for selected students',
          MESSAGE: 'Selected students status have successfully been updated'
        },
        EDIT: {
          UPDATE_MESSAGE: 'Student has been updated',
          ADD_MESSAGE: 'Student has been created'
        }
      }
    },
    TUTOR: {
      COMMON: {
        SELECTED_RECORDS_COUNT: 'Selected records count: ',
        ALL: 'All',
        SUSPENDED: 'Suspended',
        ACTIVE: 'Active',
        FILTER: 'Filter',
        BY_STATUS: 'by Status',
        BY_TYPE: 'by Type',
        BUSINESS: 'Business',
        INDIVIDUAL: 'Individual',
        SEARCH: 'Search',
        IN_ALL_FIELDS: 'in all fields',
        DELETE_ALL: 'Delete All',
        FETCH_SELECTED: 'Fetch Selected',
        UPDATE_STATUS: 'Update Status'
      },
      TUTORS: {
        TUTORS: 'Tutors',
        TUTORS_LIST: 'Tutors list',
        NEW_TUTOR: 'New Tutor',
        EDIT_TUTOR: 'Edit Tutor',
        CREATE_TUTOR: 'Create Tutor',
        TABLE: {
          COL_1: 'ID',
          COL_2: 'FIRSTNAME',
          COL_3: 'FIRSTNAME',
          COL_4: 'EMAIL',
          COL_5: 'GENDER',
          COL_6: 'STATUS',
          COL_7: 'TYPE',
          COL_8: 'ACTIONS',
        },
        FORM: {
          FULL_NAME: 'Full Name',
          UNIVERSITY_NAME: 'University Name',
          GENDER: 'Gender',
          DATE_OF_BIRTH: 'Date of birth',
          EMAIL: 'Email',
          PHONE_NUMBER: 'Phone Number',
          COUNTRY: 'Country',
          CITY: 'City',
          SUBJECTS: 'Subjects'
        },
        DELETE_TUTOR_SIMPLE: {
          TITLE: 'Student Delete',
          DESCRIPTION: 'Are you sure to permanently delete this student?',
          WAIT_DESCRIPTION: 'Student is deleting...',
          MESSAGE: 'Student has been deleted'
        },
        DELETE_TUTOR_MULTY: {
          TITLE: 'Students Delete',
          DESCRIPTION: 'Are you sure to permanently delete selected Students?',
          WAIT_DESCRIPTION: 'Students are deleting...',
          MESSAGE: 'Selected students have been deleted'
        },
        UPDATE_STATUS: {
          TITLE: 'Status has been updated for selected students',
          MESSAGE: 'Selected students status have successfully been updated'
        },
        EDIT: {
          UPDATE_MESSAGE: 'Student has been updated',
          ADD_MESSAGE: 'Student has been created'
        }
      }
    },
    COUNTRY: {
      COMMON: {
        SELECTED_RECORDS_COUNT: 'Selected records count: ',
        ALL: 'All',
        SUSPENDED: 'Suspended',
        ACTIVE: 'Active',
        FILTER: 'Filter',
        BY_STATUS: 'by Status',
        BY_TYPE: 'by Type',
        BUSINESS: 'Business',
        INDIVIDUAL: 'Individual',
        SEARCH: 'Search',
        IN_ALL_FIELDS: 'in all fields',
        DELETE_ALL: 'Delete All',
        FETCH_SELECTED: 'Fetch Selected',
        UPDATE_STATUS: 'Update Status'
      },
      COUNTRIES: {
        COUNTRIES: 'Countries',
        COUNTRIES_LIST: 'Countries list',
        NEW_COUNTRY: 'New Country',
        EDIT_COUNTRY: 'Edit Country',
        CREATE_COUNTRY: 'Create Country',
        TABLE: {
          COL_1: 'ID',
          COL_2: 'ENGLISH NAME',
          COL_3: 'ARABIC NAME',
          COL_4: 'ISO 2',
          COL_5: 'STATUS',
          COL_6: 'ACTIONS',
        },
        FORM: {
          ARABIC_NAME: 'ARABIC Name',
          ENGLISH_NAME: 'ENGLISH Name',
          ISO2: 'ISO-2'
        },
        DELETE_TUTOR_SIMPLE: {
          TITLE: 'Country Delete',
          DESCRIPTION: 'Are you sure to permanently delete this Country?',
          WAIT_DESCRIPTION: 'Country is deleting...',
          MESSAGE: '  Country has been deleted'
        },
        DELETE_TUTOR_MULTY: {
          TITLE: 'Countries Delete',
          DESCRIPTION: 'Are you sure to permanently delete selected Countries?',
          WAIT_DESCRIPTION: 'Countries are deleting...',
          MESSAGE: 'Selected Countries have been deleted'
        },
        UPDATE_STATUS: {
          TITLE: 'Status has been updated for selected Countries',
          MESSAGE: 'Selected Countries status have successfully been updated'
        },
        EDIT: {
          UPDATE_MESSAGE: 'Country has been updated',
          ADD_MESSAGE: 'Country has been created'
        }
      }
    },
    CITY: {
      COMMON: {
        SELECTED_RECORDS_COUNT: 'Selected records count: ',
        ALL: 'All',
        SUSPENDED: 'Suspended',
        ACTIVE: 'Active',
        FILTER: 'Filter',
        BY_STATUS: 'by Status',
        BY_TYPE: 'by Type',
        BUSINESS: 'Business',
        INDIVIDUAL: 'Individual',
        SEARCH: 'Search',
        IN_ALL_FIELDS: 'in all fields',
        DELETE_ALL: 'Delete All',
        FETCH_SELECTED: 'Fetch Selected',
        UPDATE_STATUS: 'Update Status'
      },
      CITIES: {
        CITIES: 'Cities',
        CITIES_LIST: 'Cities list',
        NEW_CITY: 'New City',
        EDIT_CITY: 'Edit City',
        CREATE_CITY: 'Create City',
        TABLE: {
          COL_1: 'ID',
          COL_2: 'ENGLISH NAME',
          COL_3: 'ARABICNAME',
          COL_4: 'Country',
          COL_5: 'STATUS',
          COL_6: 'ACTIONS',
        },
        FORM: {
          ARABIC_NAME: 'ARABIC Name',
          ENGLISH_NAME: 'ENGLISH Name',
          COUNTRY: 'Country'
        },
        DELETE_TUTOR_SIMPLE: {
          TITLE: 'City Delete',
          DESCRIPTION: 'Are you sure to permanently delete this city?',
          WAIT_DESCRIPTION: 'CITY is deleting...',
          MESSAGE: '  City has been deleted'
        },
        DELETE_TUTOR_MULTY: {
          TITLE: 'Cities Delete',
          DESCRIPTION: 'Are you sure to permanently delete selected Cities?',
          WAIT_DESCRIPTION: 'Cities are deleting...',
          MESSAGE: 'Selected cities have been deleted'
        },
        UPDATE_STATUS: {
          TITLE: 'Status has been updated for selected cities',
          MESSAGE: 'Selected cities status have successfully been updated'
        },
        EDIT: {
          UPDATE_MESSAGE: 'City has been updated',
          ADD_MESSAGE: 'City has been created'
        }
      }
    },



    ECOMMERCE: {
      COMMON: {
        SELECTED_RECORDS_COUNT: 'Selected records count: ',
        ALL: 'All',
        SUSPENDED: 'Suspended',
        ACTIVE: 'Active',
        FILTER: 'Filter',
        BY_STATUS: 'by Status',
        BY_TYPE: 'by Type',
        BUSINESS: 'Business',
        INDIVIDUAL: 'Individual',
        SEARCH: 'Search',
        IN_ALL_FIELDS: 'in all fields'
      },
      ECOMMERCE: 'eCommerce',
      CUSTOMERS: {
        CUSTOMERS: 'Customers',
        CUSTOMERS_LIST: 'Customers list',
        NEW_CUSTOMER: 'New Customer',
        DELETE_CUSTOMER_SIMPLE: {
          TITLE: 'Customer Delete',
          DESCRIPTION: 'Are you sure to permanently delete this customer?',
          WAIT_DESCRIPTION: 'Customer is deleting...',
          MESSAGE: 'Customer has been deleted'
        },
        DELETE_CUSTOMER_MULTY: {
          TITLE: 'Customers Delete',
          DESCRIPTION: 'Are you sure to permanently delete selected customers?',
          WAIT_DESCRIPTION: 'Customers are deleting...',
          MESSAGE: 'Selected customers have been deleted'
        },
        UPDATE_STATUS: {
          TITLE: 'Status has been updated for selected customers',
          MESSAGE: 'Selected customers status have successfully been updated'
        },
        EDIT: {
          UPDATE_MESSAGE: 'Customer has been updated',
          ADD_MESSAGE: 'Customer has been created'
        }
      }
    }
  }
};
