import { React} from 'react'
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import CurrenciesMenu from './components/CurrenciesMenu';
import CurrencyInformation from './components/CurrencyInformation';
import Home from './components/Home';
import { CurrentCurrencyProvider } from './components/CurrentCurrencyProvider';
import { AvailableCurrenciesProvider } from './components/AvailableCurrenciesProvider';
import './App.css'

function App() {
  return (
    <>
      <AvailableCurrenciesProvider>
        <CurrentCurrencyProvider>
          <BrowserRouter>
            <Routes>
              <Route path='/' element={<CurrenciesMenu />}>
                <Route index element={<Home />} />
                <Route path='currencies' >
                  <Route index element={<Home />} />
                  <Route path=':currencyName' element={<CurrencyInformation />} />
                </Route>
              </Route>
            </Routes>
          </BrowserRouter>
        </CurrentCurrencyProvider>
      </AvailableCurrenciesProvider>
    </>
  )
}

export default App
