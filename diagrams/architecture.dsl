workspace {
  model {
    bankManager = person "Bank Manager"

    handOfZeusService = person "Hand of Zeus" "Simulates time events"
    personaService = softwareSystem "Persona Service" "Manages personas" "External System"
    otherBanks = softwareSystem "Other banks" "Commercial and any other banks" "External System"
    businesses = softwareSystem "Businesses" "Businesses with debit order needs" "External System"

    softwareSystem = softwareSystem "Apitel Bank" {
      bankManagerPortalService = container "Bank Manager Portal" "React and TS" "Nginx" {
        bankManager -> this "Views reports with" "Web Browser"
      }

      mainBankingService = container "Main Banking Service" "Apache Camel, Spring and Java" "JVM" {
        this -> personaService "Verifies customer identity with" "HTTP/TCP"
      }

      reportingService = container "Reporting Service" "Apache Camel, Spring and Java" "JVM" {
        bankManagerPortalService -> this "Gets reports from" "HTTP/TCP"
      }

      apitelBankPartnerService = container "Apitel Bank Partner Service" "C#" ".NET 8.0" {
        bankManagerPortalService -> this "Registers partners and updates access levels with" "HTTP/TCP"
        handOfZeusService -> this "Notifes of time changes" "HTTP/TCP"
        personaService -> this "Performs user actions with" "HTTP/TCP"
        otherBanks -> this "Perform interbank EFT transactions with" "HTTP/TCP"
        businesses -> this "Perform debit order services with" "HTTP/TCP"
      }

      apitelMessagingService = container "Apitel Messaging Service" "AWS SQS" {
        mainBankingService -> this "Gets and writes messages from/to" "Camel/TCP"
        reportingService -> this "Gets and writes messages from/to" "Camel/TCP"
        apitelBankPartnerService -> this "Gets and writes messages from/to" "Camel/TCP"
      }

      database = container "Database" "MS-SQL" {
        mainBankingService -> this "Persists data with" "T-SQL/TCP"
        reportingService -> this "Reads raw data from" "T-SQL/TCP"
        apitelBankPartnerService -> this "Gets/Writes authoriation levels from/to" "T-SQL/TCP"
      }
    }
  }

  views {
    systemContext softwareSystem {
      include *
      autolayout lr
    }

    container softwareSystem {
      include *
      autolayout lr
    }

    theme default

    styles {
      element "External System" {
        background #999999
        color #ffffff
      }
            
    }
  }
}