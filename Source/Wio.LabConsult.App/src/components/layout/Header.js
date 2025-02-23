import React, { Fragment } from 'react'
import '../../App.css'


const Header = () => {
  return (
    <Fragment>
        <nav className="navbar row">
            <div className="col-12 col-md-3">
                <div className="navbar-brand">
                    {/* <img src="/images/logo.png" /> */}
                    <span className="navbar-brand-text">Lab.Consultas</span>
                </div>
            </div>
            <div className="col-12 col-md-6 mt-2 mt-md-0">
                <div className="input-group">
                    {/* <input
                        type="text"
                        id="search_field"
                        className="form-control"
                        placeholder="Procurar por especialidade, médico ou exame."   
                    />
                    <div className="input-group-append">
                        <button id="search_btn" className="btn">
                            <i className="fa fa-search" aria-hidden="true"></i>
                        </button>
                    </div> */}
                      <h2>
                          <span className="gradient-text">Economize até 50% </span>
                          <span>adquira o plano!</span>
                      </h2>
                  </div>
              </div>

            <div className="col-12 col-md-3 mt-4 mt-md-0 text-center">
                <button className="btn" id="login_btn">Entrar</button>
                <span id="cart" className="ml-3">Pedido</span>
                <span className="ml-1" id="cart_count">2</span>
            </div>
       
        </nav>
    </Fragment>
  )
}

export default Header