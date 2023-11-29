export default function Heros() {
  return (
    <div>
      <div className='d-none d-lg-block'>
          <div className='row g-0 mt-5'>
              <div className='col-sm-6 col-md-6'>
                  <div className='col-image-left'></div>
              </div>
              <div className='col-4 col-md-4 container d-flex justify-content-center align-items-center'>
                  <div className='ml-2'>
                      <h1>Always up-to-date timetable</h1>
                      <p className='lead'>
                      You always have the best, most complete and up-to-date 
                      information about train running. 
                      Along with the timetable information, 
                      you will also learn the price of your connection.
                      </p>
                  </div>
              </div>
          </div>
          <div className='row g-0'>
              <div className='col-4 col-md-4 container d-flex 
                  justify-content-center align-items-center'>
                  <div className='ml-2'>
                      <h1>Best price guarantee</h1>
                      <p className='lead'>
                      Don't miss any relief, discount or promotion. 
                      You will find out exactly how much your ticket costs - for you, 
                      your family and friends - always with the best price guarantee!
                      </p>
                  </div>
              </div>
              <div className='col-sm-6 col-md-6'>
                  <div className='col-image-right'></div>
              </div>
          </div>
      </div>
    </div>
  );
}