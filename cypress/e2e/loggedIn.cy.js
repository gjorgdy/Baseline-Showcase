describe('Logged In', () => {
  beforeEach(() => {
    cy.intercept('GET', 'https://api.hexasis.eu/users/me', {
      statusCode: 200,
      body: { userId: 0 },
      headers: {
        'Content-Type': 'application/json', // Set the Content-Type header to application/json
      },
    }).as('mockedRequest')
    
    cy.intercept('GET', 'https://api.hexasis.eu/users/0/profile', {
      statusCode: 200,
      body: { 
        user: {
          id: 0,
          displayName: 'Test User',
          profilePicture: '/resources/default_pfp.jpg',
        },
        tiles: [],
        canEdit: true,
      },
      headers: {
        'Content-Type': 'application/json', // Set the Content-Type header to application/json
      },
    }).as('mockedRequest')
    
    cy.visit('https://baseline.hexasis.eu/')
    // Accept cookies
    // cy.get('button').click()
    cy.wait(1000);
  })

  it('Display account pop-up if logged in', () => {
    cy.get('#navAccount').click()
    cy.contains('Log out')
        .parent()
        .should('be.visible')
  })

  it('Add a tile pop-up', () => {
    cy.get('#navAccount').click()
    cy.get('#myProfile').click()
    cy.get('.button').click()
    cy.get('.modal')
        .should('be.visible')
  })
})